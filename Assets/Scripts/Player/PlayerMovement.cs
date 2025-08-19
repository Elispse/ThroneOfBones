using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float hSpeed = 10f;
    [SerializeField] private float vSpeed = 6f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravity = 20f;
    private float jumpHeight = 0f;
    private float jumpVelocity = 0f;
    private bool isJumping = false;

    [Header("References")]
    [SerializeField] private Transform playerObject; // Sprite & hitboxes live here
    [SerializeField] private Transform shadow;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Rigidbody2D component not found on Player.");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer not found on Player.");

        if (playerObject == null)
            Debug.LogError("Assign a Visuals transform (child object).");
    }

    private void Update()
    {
        HandleJump();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 targetVelocity = new Vector3(input.x * hSpeed, input.y * vSpeed);

        rb.linearVelocity = targetVelocity;

        if (rb.linearVelocityX > 0 && !facingRight)
            Flip();
        else if (rb.linearVelocityX < 0 && facingRight)
            Flip();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping)
        {
            isJumping = true;
            jumpVelocity = jumpForce;
        }
    }

    private void HandleJump()
    {
        if (!isJumping) return;

        // Apply fake gravity
        jumpVelocity -= gravity * Time.deltaTime;
        jumpHeight += jumpVelocity * Time.deltaTime;

        // Land
        if (jumpHeight <= 0f)
        {
            jumpHeight = 0f;
            isJumping = false;
        }

        // Offset visuals upward
        playerObject.localPosition = new Vector3(0f, jumpHeight, 0f);

        // Keep shadow on ground (optional)
        if (shadow != null)
        {
            float scale = Mathf.Lerp(1f, 0.4f, jumpHeight / jumpForce);
            shadow.localScale = new Vector3(scale, scale, 1f);
        }
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX; // flip horizontally, not Y
        facingRight = !facingRight;
    }
}
