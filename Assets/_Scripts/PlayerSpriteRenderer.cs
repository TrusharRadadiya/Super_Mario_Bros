using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    private PlayerMovement _playerMovement;

    [SerializeField] private Sprite _idle;
    [SerializeField] private Sprite _jump;
    [SerializeField] private Sprite _slide;
    [SerializeField] private AnimatedSprite _run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        _run.enabled = false;
    }

    private void LateUpdate()
    {
        _run.enabled = _playerMovement.running;

        if (_playerMovement.jumping) spriteRenderer.sprite = _jump;
        else if (_playerMovement.sliding) spriteRenderer.sprite = _slide;        
        else if (!_playerMovement.running) spriteRenderer.sprite = _idle;
    }
}
