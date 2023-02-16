using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    public Vector2 direction = Vector2.left;
    public float speed = 2f;

    public bool cameraAwareness { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
        cameraAwareness = true;
    }

    private void OnBecameVisible()
    {
        if (cameraAwareness) enabled = true;
    }

    private void OnBecameInvisible()
    {
        if (cameraAwareness) enabled = false;
    }

    private void OnEnable()
    {
        _rigidbody.WakeUp();
    }

    private void OnDisable()
    {
        _velocity = Vector2.zero;
        _rigidbody.Sleep();
    }

    private void FixedUpdate()
    {
        _velocity.x = direction.x * speed;
        _velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);

        if (_rigidbody.Raycast(direction)) direction = -direction;
        if (_rigidbody.Raycast(Vector2.down)) _velocity.y = Mathf.Max(_velocity.y, 0f);
    }
}
