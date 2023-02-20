using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _speed = .1f;
    [SerializeField] private bool _randomizeSpeed = false;    

    private void Start()
    {
        _speed = _randomizeSpeed ? Random.Range(.1f, .5f) : _speed;
    }

    private void Update()
    {
        transform.position += Vector3.right * _speed * Time.deltaTime;
    }
}
