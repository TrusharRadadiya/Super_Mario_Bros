using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _height;
    [SerializeField] private float _undergroundHeight;

    private void LateUpdate()
    {
        var cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, _player.position.x);
        transform.position = cameraPosition;
    }

    public void SetUndergroundCamera(bool underground)
    {
        var cameraPosition = transform.position;
        cameraPosition.y = underground ? _undergroundHeight : _height;
        transform.position = cameraPosition;
    }
}
