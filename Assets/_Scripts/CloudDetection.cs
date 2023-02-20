using UnityEngine;

public class CloudDetection : MonoBehaviour
{
    [SerializeField] private Transform _resetter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            var position = collision.transform.position;
            position.x = _resetter.position.x;
            collision.transform.position = position;
        }
    }
}
