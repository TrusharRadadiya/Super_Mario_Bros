using UnityEngine;
using DG.Tweening;

public class BlockHit : MonoBehaviour
{
    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private int _maxHits = -1;
    [SerializeField] private Ease _ease;
    private bool _animating;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_maxHits == 0) return;

        if (!_animating && collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.DotProduct(transform, Vector2.up)) HitTheBlock();
        }
    }

    private void HitTheBlock()
    {
        _maxHits--;
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (_maxHits == 0) spriteRenderer.sprite = _emptySprite;

        Animate();
    }

    private void Animate()
    {
        _animating = true;

        var originalPosition = transform.localPosition;
        var animatedPosition = transform.localPosition + Vector3.up * .5f;
        var time = .15f;
        DOTween.Sequence()
            .Append(transform.DOLocalMove(animatedPosition, time).SetEase(_ease))
            .Append(transform.DOLocalMove(originalPosition, time).SetEase(_ease))
            .AppendCallback(() => { transform.localPosition = originalPosition; _animating = false; });
    }
}
