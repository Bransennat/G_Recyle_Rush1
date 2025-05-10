using UnityEngine;

public class test_with_pc_keyboard : MonoBehaviour
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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private float moveDirection = 0f;

    void Update()
    {
        moveDirection = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection = -1f;
            Debug.Log("Moving Left");
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection = 1f;
            Debug.Log("Moving Right");
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            Debug.Log("Jump");
        }

        if (animator != null)
        {
            animator.SetFloat(speedParam, Mathf.Abs(rb.linearVelocity.x));
            animator.SetBool(jumpParam, !isGrounded);
        }

        if (moveDirection != 0 && spriteRenderer != null)
        {
            spriteRenderer.flipX = moveDirection < 0;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
