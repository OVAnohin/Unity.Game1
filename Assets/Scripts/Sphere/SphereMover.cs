using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SphereMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _zAngle;
    [SerializeField] private float _jumpForce;

    private Rigidbody _rigidbody;
    private bool _isJumping = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _isJumping == false)
        {
            _rigidbody.velocity = Vector3.up * _jumpForce;
            _isJumping = true;
        }

        transform.position = transform.position + Vector3.right * _speed * Time.deltaTime;
        transform.Rotate(0, 0, _zAngle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isJumping = false;
    }
}
