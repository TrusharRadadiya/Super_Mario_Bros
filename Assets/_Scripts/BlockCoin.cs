using DG.Tweening;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    [SerializeField] private Ease _ease;

    private void Start()
    {
        Animate();
    }

    private void Animate()
    {
        var originalPosition = transform.localPosition;
        var animatedPosition = transform.localPosition + Vector3.up * 2f;
        var time = .25f;
        DOTween.Sequence()
            .Append(transform.DOLocalMove(animatedPosition, time).SetEase(_ease))
            .Append(transform.DOLocalMove(originalPosition, time))
            .AppendCallback(() => { transform.localPosition = originalPosition; });
    }
}
