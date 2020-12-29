using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce;

    private bool _isJumping = false;
    private Rigidbody _rigidbody;

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

        transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime, Space.World);

        transform.rotation *= Quaternion.Euler(0, 0, _rotateSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isJumping = false;
    }
}
