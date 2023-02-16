using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSpriteRenderer _smallRenderer;
    [SerializeField] private PlayerSpriteRenderer _bigRenderer;
    private DeathAnimation _deathAnimation;
    private CapsuleCollider2D _capsuleCollider;

    private bool _big = false;
    private bool dead => _deathAnimation.enabled;
    public bool starpower { get; private set; }

    private void Awake()
    {
        _deathAnimation = GetComponent<DeathAnimation>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    public void Hit()
    {
        if (!dead && starpower) return;

        if (_big) Shrink();
        else Death();
    }

    private void Shrink()
    {
        _bigRenderer.enabled = false;
        _smallRenderer.enabled = true;       
        _big = false;

        _capsuleCollider.size = new Vector2(1f, 1f);
        _capsuleCollider.offset = new Vector2(0f, 0f);
        StartCoroutine(ScaleAnimation());
    }

    public void Grow()
    {
        _smallRenderer.enabled = false;
        _bigRenderer.enabled = true;
        _big = true;

        _capsuleCollider.size = new Vector2(1f, 2f);
        _capsuleCollider.offset = new Vector2(0f, .5f);
        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsedTime = 0f;
        float totalTime = 1f;

        while (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;

            if (Time.frameCount % 5 == 0)
            {
                _smallRenderer.enabled = !_smallRenderer.enabled;
                _bigRenderer.enabled = !_smallRenderer.enabled;
            }
            yield return null;
        }

        if (_big)
        {
            _bigRenderer.enabled = true;
            _smallRenderer.enabled = false;
        }
        else
        {
            _smallRenderer.enabled = true;
            _bigRenderer.enabled = false;
        }
    }

    private void Death()
    {
        _smallRenderer.enabled = _bigRenderer.enabled = false;
        _deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(2f);
    }

    public void Starpower(float duration = 10f)
    {
        starpower = true;
        StartCoroutine(StarpowerAnimation(duration));
    }

    private IEnumerator StarpowerAnimation(float duration)
    {
        var playerSpriteRenderer = _big ? _bigRenderer : _smallRenderer;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            if (Time.frameCount % 5 == 0)
                playerSpriteRenderer.spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);            
            yield return null;
        }

        playerSpriteRenderer.spriteRenderer.color = Color.white;        
        starpower = false;
    }
}
