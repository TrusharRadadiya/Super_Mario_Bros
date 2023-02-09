using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void LateUpdate()
    {
        var cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, _player.position.x);
        transform.position = cameraPosition;
    }
}
