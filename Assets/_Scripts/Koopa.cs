using UnityEngine;

public class Koopa : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _shellSprite;

    private bool _hiddenInShell;
    private bool _pushed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<Player>();
            if (player.starpower) Hit();
            else if (!_hiddenInShell)
            {
                if (collision.transform.DotProduct(transform, Vector2.down)) HideInShell();
                else player.Hit();
            }
            else
            {
                if (!_pushed)
                {
                    var direction = new Vector2(transform.position.x - player.transform.position.x, 0f);
                    PushTheShell(direction);
                }
                else player.Hit();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var animatedSprite = collision.gameObject.GetComponent<AnimatedSprite>();
            if (animatedSprite != null) animatedSprite.enabled = false;

            var deathAnimation = collision.gameObject.GetComponent<DeathAnimation>();
            if (deathAnimation != null) deathAnimation.enabled = true;
        }
    }

    private void HideInShell()
    {
        _hiddenInShell = true;
        GetComponent<EntityMovement>().enabled = false;
        GetComponent<AnimatedSprite>().enabled = false;
        _spriteRenderer.sprite = _shellSprite;
    }

    private void PushTheShell(Vector2 direction)
    {
        _pushed = true;
        GetComponent<Rigidbody2D>().isKinematic = false;

        var entityMovement = GetComponent<EntityMovement>();
        entityMovement.direction = direction;
        entityMovement.speed = 15f;
        entityMovement.enabled = true;
        entityMovement.cameraAwareness = false;

        gameObject.layer = LayerMask.NameToLayer("Shell");
    }

    private void Hit()
    {
        GetComponent<AnimatedSprite>().enabled = false;
        GetComponent<DeathAnimation>().enabled = true;
        Destroy(gameObject, 2f);
    }
}
