using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    private bool _isJumping = false;
    private Rigidbody _playerRigidbody;
    private float _turnAngle = -10f;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && _isJumping == false)
        {
            _playerRigidbody.velocity = Vector3.up * _jumpForce;
            _isJumping = true;
        }

        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = Vector3.right * _movementSpeed * Time.deltaTime;
        _playerRigidbody.MovePosition(transform.position + movement);
    }

    private void Turn()
    {
        Quaternion turn = _playerRigidbody.rotation * Quaternion.Euler(0, 0, _turnAngle);
        Quaternion rotation = Quaternion.Slerp(_playerRigidbody.rotation, turn, _rotationSpeed);
        _playerRigidbody.MoveRotation(rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isJumping = false;
    }
}
