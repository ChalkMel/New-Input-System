using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [Header("Ground check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundCheckMask;

    private Rigidbody _rb;
    private bool _isGrounded;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGround();
    }
    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(
             groundCheck.position,
             groundCheckRadius,
             groundCheckMask);
    }

    private void OnDrawGizmos()
    {
            Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }
    public void Move(Vector2 movement)
    {
        Debug.Log(movement);
        Vector3 direction = new Vector3 (movement.x, 0, movement.y);
        Debug.Log(direction);
        Vector3 velocity = direction * speed;
        velocity.y = _rb.linearVelocity.y;
        _rb.linearVelocity = velocity;
    }

    public void Jump()
    {
        if (_isGrounded == true)
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
