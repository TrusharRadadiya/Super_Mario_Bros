using DG.Tweening;
using System.Collections;
using UnityEngine;

public class BlockItem : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        var rigidbody = GetComponent<Rigidbody2D>();
        var circleCollider = GetComponent<CircleCollider2D>();
        var boxCollider = GetComponent<BoxCollider2D>();
        var spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
        circleCollider.enabled = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(.25f);
        spriteRenderer.enabled = true;

        var startPosition = transform.localPosition;
        var endPosition = transform.localPosition + Vector3.up;
        DOTween.Sequence()
            .Append(transform.DOLocalMove(endPosition, .5f).SetEase(Ease.OutSine))
            .AppendCallback(() =>
            {
                transform.localPosition = endPosition;
                circleCollider.enabled = true;
                boxCollider.enabled = true;
                rigidbody.isKinematic = false;
            });
    }
}
