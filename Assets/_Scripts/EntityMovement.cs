using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private Vector2 _direction = Vector2.left;
    [SerializeField] private float _speed = 2f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }

    private void OnBecameInvisible()
    {
        enabled = false;
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
        _velocity.x = _direction.x * _speed;
        _velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.fixedDeltaTime);

        if (_rigidbody.Raycast(_direction)) _direction = -_direction;
        if (_rigidbody.Raycast(Vector2.down)) _velocity.y = Mathf.Max(_velocity.y, 0f);
    }
}
