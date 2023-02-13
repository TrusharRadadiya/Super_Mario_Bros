using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    private float _frameRate = 1f / 6f;
    private int _frame = 0;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), _frameRate, _frameRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        _frame++;
        if (_frame >= _sprites.Length) _frame = 0;

        if (_frame >= 0 && _frame < _sprites.Length) _spriteRenderer.sprite = _sprites[_frame];
    }
}
