using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private KeyCode _enteringKey = KeyCode.S;
    [SerializeField] private KeyCode _altEnteringKey = KeyCode.DownArrow;
    
    [Space, SerializeField] private Vector3 _enterDirection = Vector3.down;
    [SerializeField] private Vector3 _exitDirection = Vector3.zero;
    [SerializeField] private Transform _exitPoint;

    private bool _entering = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_exitPoint == null) return;

        if (collision.CompareTag("Player"))
        {
            if (!_entering && (Input.GetKeyDown(_enteringKey) || Input.GetKeyDown(_altEnteringKey)))
            {
                collision.GetComponent<PlayerMovement>().enabled = false;
                StartCoroutine(PipeTravel(collision.transform));
            }
        }
    }

    private IEnumerator PipeTravel(Transform player)
    {
        Enter(player);
        yield return new WaitForSeconds(1f);

        bool underground = _exitPoint.position.y < -5f;
        Camera.main.GetComponent<SideScrolling>().SetUndergroundCamera(underground);

        if (_exitDirection != Vector3.zero) Exit(player);
        else Teleport(player);
    }

    private void Enter(Transform player)
    {
        _entering = true;

        var enteringPosition = player.position + _enterDirection;
        player.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack);
        player.DOMove(enteringPosition, .7f).SetDelay(.3f);
    }

    private void Teleport(Transform player)
    {
        player.position = _exitPoint.position;
        player.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            player.localScale = Vector3.one;
            player.GetComponent<PlayerMovement>().enabled = true;
            _entering = false;
        });
    }

    private void Exit(Transform player)
    {
        player.position = _exitPoint.position;
        var exitingPosition = _exitPoint.position + _exitDirection;

        player.DOScale(Vector3.one, .5f).SetEase(Ease.OutBack);
        player.DOMove(exitingPosition, .7f).SetDelay(.3f).OnComplete(() =>
        {
            player.localScale = Vector3.one;
            player.GetComponent<PlayerMovement>().enabled = true;
            _entering = false;
        });
    }
}
