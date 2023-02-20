using DG.Tweening;
using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    [SerializeField] private Transform _flag;
    [SerializeField] private Transform _bottom;
    [SerializeField] private Transform _castle;
    [SerializeField] private float _speed = 6f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(_flag, _bottom.position));
            StartCoroutine(LevelCompleteSequence(collision.transform));
        }
    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > .1f)
        {
            subject.position = Vector3.MoveTowards(subject.position, destination, _speed * Time.deltaTime);
            yield return null;
        }
        subject.position = destination;        
    }

    private IEnumerator RotateAndMove(Transform subject, Vector3 rotation, Vector3 destination)
    {
        StartCoroutine(MoveTo(subject, destination));

        subject.DORotate(rotation, .2f);
        yield return new WaitForSeconds(.2f);
        var rot = subject.rotation;
        rot.eulerAngles = rotation;
        subject.rotation = rot;
    }

    private IEnumerator LevelCompleteSequence(Transform player)
    {
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        yield return MoveTo(player, _bottom.position);
        yield return RotateAndMove(player, new Vector3(0f, 180f, 0f), player.position + Vector3.right);
        yield return RotateAndMove(player, new Vector3(0f, 0f, 0f), player.position + Vector3.right + Vector3.down);

        playerMovement.enabled = true;
        GetComponent<Collider2D>().isTrigger = false;
    }
}
