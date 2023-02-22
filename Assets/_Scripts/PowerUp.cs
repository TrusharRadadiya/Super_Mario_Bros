using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerType
    {
        Coin,
        ExtraLife,
        MagicMushroom,
        Starman
    }

    [SerializeField] private PowerType _type;
    [SerializeField] private AudioClip _powerUpClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Collect(collision.gameObject);        
    }

    private void Collect(GameObject player)
    {
        switch (_type)
        {
            case PowerType.Coin:
                GameManager.Instance.AddCoins();
                break;

            case PowerType.ExtraLife:
                GameManager.Instance.AddLives();
                break;

            case PowerType.MagicMushroom:
                player.GetComponent<Player>().Grow();
                break;

            case PowerType.Starman:
                player.GetComponent<Player>().Starpower();
                break;
        }
        SoundManager.Instance.PlayEffect(_powerUpClip);
        Destroy(gameObject);
    }
}
