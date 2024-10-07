using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] [Range(0, 50)] private float _cameraSpeed;
    private Transform _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.position = new Vector3(_player.position.x, _player.position.y, _player.position.z - 10);
    }

    private void Update()
    {
        if (_player)
        {
            Vector3 target = new Vector3(_player.position.x, _player.position.y, _player.position.z - 10);

            Vector3 pos = Vector3.Lerp(transform.position, target, _cameraSpeed * Time.deltaTime);

            transform.position = pos;
        }
    }
}
