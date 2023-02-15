using UnityEngine;
using System.Collections;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _deadSprite;

    private void Awake()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sortingOrder = 10;
        if (_deadSprite != null) _spriteRenderer.sprite = _deadSprite;
    }

    private void DisablePhysics()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders) collider.enabled = false;

        GetComponent<Rigidbody2D>().isKinematic = true;

        var playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement != null) playerMovement.enabled = false;
        var entityMovement = GetComponent<EntityMovement>();
        if (entityMovement != null) entityMovement.enabled = false;
    }

    private IEnumerator Animate()
    {
        float jumpForce = 10f;
        float gravity = -36f;
        float elapsedTime = 0f;
        float jumpTime = 2f;
        Vector3 velocity = Vector3.up * jumpForce;
        
        while (elapsedTime < jumpTime)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
