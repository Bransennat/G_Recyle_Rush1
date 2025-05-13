using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Game Reference")]
    public GameManager gameManager;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Animation Settings")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public string speedParam = "speed";
    public string jumpParam = "isJumping";

    private Rigidbody2D rb;
    private bool isGrounded = false;

    [SerializeField] private InputActionReference moveActionToUse;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (moveActionToUse != null && moveActionToUse.action != null)
        {
            moveActionToUse.action.Enable(); // Enable the InputAction
        }
    }

    void Update()
    {
        if (moveActionToUse != null && moveActionToUse.action != null)
    {
        // Read joystick input
        Vector2 inputDirection = moveActionToUse.action.ReadValue<Vector2>();

        // Apply movement to Rigidbody
        rb.linearVelocity = new Vector2(inputDirection.x * moveSpeed, rb.linearVelocity.y); // Corrected to use rb.velocity

        // Update animations
        if (animator != null)
        {
            animator.SetFloat(speedParam, Mathf.Abs(rb.linearVelocity.x));
            animator.SetBool(jumpParam, !isGrounded);
        }

        // Flip sprite based on movement direction
        if (inputDirection.x != 0 && spriteRenderer != null)
        {
            spriteRenderer.flipX = inputDirection.x > 0;
        }
    }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Corrected to use rb.velocity
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}