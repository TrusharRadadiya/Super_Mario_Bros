using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSpriteRenderer _smallRenderer;
    [SerializeField] private PlayerSpriteRenderer _bigRenderer;
    private DeathAnimation _deathAnimation;

    private bool big => _bigRenderer.enabled;
    private bool dead => _deathAnimation.enabled;

    private void Awake()
    {
        _deathAnimation = GetComponent<DeathAnimation>();
    }

    public void Hit()
    {
        if (big) Shrink();
        else Death();
    }

    private void Shrink() { }

    private void Death()
    {
        _smallRenderer.enabled = _bigRenderer.enabled = false;
        _deathAnimation.enabled = true;

        GameManager.Instance.ResetLevel(2f);
    }
}
