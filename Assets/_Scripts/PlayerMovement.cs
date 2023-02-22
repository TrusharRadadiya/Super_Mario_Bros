using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private Camera _camera;
    private float _inputAxis;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float maxJumpHeight = 5f;
    [SerializeField] private float maxJumpTime = 1f;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private ParticleSystem _jumpDust;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _stompClip;

    public float jumpForce => 2f * maxJumpHeight / (maxJumpTime * .5f);
    public float gravity => -2f * maxJumpHeight / Mathf.Pow(maxJumpTime * .5f, 2);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(_velocity.x) > 0.25f || Mathf.Abs(_inputAxis) > 0.25f;
    public bool sliding => (_velocity.x > 0f && _inputAxis < 0f) || (_velocity.x < 0f && _inputAxis > 0f);

    private bool _facingRight = true;
    private bool _playDust = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _velocity = Vector2.zero;   
    }

    private void OnDisable()
    {
        _velocity = Vector2.zero;
    }

    private void Update()
    {
        HorizontalMovement();

        if (sliding)
        {
            if (!_playDust)
            {
                _playDust = true;
                PlayDustParticle();
            }
        }
        else if (_playDust) _playDust = false;        

        grounded = _rigidbody.Raycast(Vector2.down);
        if (grounded) GroundedMovement();

        ApplyGravity();
    }

    private void FixedUpdate()
    {
        var position = _rigidbody.position;
        position += _velocity * Time.fixedDeltaTime;

        var leftEdge = _camera.ScreenToWorldPoint(Vector2.zero);
        var rightEdge = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + .5f, rightEdge.x - .5f);

        _rigidbody.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        _inputAxis = Input.GetAxis("Horizontal");
        _velocity.x = Mathf.MoveTowards(_velocity.x, _inputAxis * moveSpeed, moveSpeed * Time.deltaTime);

        if (_rigidbody.Raycast(Vector2.right * _velocity.x)) _velocity.x = 0f;

        if (_velocity.x > 0f)
        {
            _facingRight = true;
            transform.eulerAngles = Vector3.zero;
        }
        else if (_velocity.x < 0f)
        {
            _facingRight = false;
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void GroundedMovement()
    {
        _velocity.y = Mathf.Max(_velocity.y, 0f);
        jumping = _velocity.y > 0f;

        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SoundManager.Instance.PlayEffect(_jumpClip);
            _velocity.y = jumpForce;
            jumping = true;
            PlayJumpDustParticle();   
        }
    }

    private void ApplyGravity()
    {
        bool falling = _velocity.y < 0f || !Input.GetButton("Jump");
        float multiplier = falling ? 2f : 1f;

        _velocity.y += gravity * multiplier * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (transform.DotProduct(collision.transform, Vector2.down))
            {
                SoundManager.Instance.PlayEffect(_stompClip);
                _velocity.y = jumpForce * .5f;
                jumping = true;
                PlayJumpDustParticle();
            }
            else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
            {
                if (transform.DotProduct(collision.transform, Vector2.up)) _velocity.y = 0f;
            }
        }
    }

    private void PlayDustParticle() => _dust.Play();

    private void PlayJumpDustParticle()
    {
        var jumpDust = Instantiate(_jumpDust, _jumpDust.transform.position, Quaternion.identity);
        jumpDust.Play();
        Destroy(jumpDust.gameObject, 1f);
    }
}
