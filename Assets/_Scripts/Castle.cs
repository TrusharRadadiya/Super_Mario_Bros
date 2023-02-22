using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private AudioClip _stageClearClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlayEffect(_stageClearClip);
            collision.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(_stageClearClip.length);
        }
    }
}
