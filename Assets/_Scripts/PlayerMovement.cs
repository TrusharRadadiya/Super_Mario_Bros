using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _velocity;
    private Camera _camera;

    public float moveSpeed = 8f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void Update()
    {
        HorizontalMovement();
    }

    private void FixedUpdate()
    {
        var position = _rb.position;
        position += _velocity * Time.fixedDeltaTime;

        var leftEdge = _camera.ScreenToWorldPoint(Vector2.zero);
        var rightEdge = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + .5f, rightEdge.x - .5f);

        _rb.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        var inputAxis = Input.GetAxis("Horizontal");
        _velocity.x = Mathf.MoveTowards(_velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }
}
