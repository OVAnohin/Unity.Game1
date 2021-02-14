using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
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

        _rigidbody.velocity = new Vector3(_speed * Time.deltaTime, _rigidbody.velocity.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isJumping = false;
    }
}
