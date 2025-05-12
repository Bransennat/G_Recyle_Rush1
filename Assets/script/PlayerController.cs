using UnityEngine;

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
    private int moveDirection = 0;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);

        if (animator != null)
        {
            animator.SetFloat(speedParam, Mathf.Abs(rb.linearVelocity.x));
            animator.SetBool(jumpParam, !isGrounded);
        }

        if (moveDirection != 0 && spriteRenderer != null)
        {
            spriteRenderer.flipX = moveDirection > 0;
        }
    }

    public void MoveLeft() => moveDirection = -1;
    public void MoveRight() => moveDirection = 1;
    public void StopMoving() => moveDirection = 0;

    public void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
