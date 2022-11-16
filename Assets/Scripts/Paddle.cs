using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _maxMovement = 2.0f;

    private void Update()
    {
        float input = Input.GetAxis("Horizontal");
        var pos = transform.position;

        pos.x += input * _speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -_maxMovement, _maxMovement);

        transform.position = pos;
    }
}
