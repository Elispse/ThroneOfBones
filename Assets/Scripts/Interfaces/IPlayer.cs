using UnityEngine;
using UnityEngine.InputSystem;

public abstract class IPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] protected float hSpeed = 10f;
    [SerializeField] protected float vSpeed = 6f;

    [Header("Jump Settings")]
    [SerializeField] protected float jumpForce = 10f;
    [SerializeField] protected float gravity = 20f;
    protected float jumpHeight = 0f;
    protected float jumpVelocity = 0f;
    protected bool isJumping = false;

    [Header("References")]
    [SerializeField] protected Transform playerObject;
    [SerializeField] protected Transform shadow;

    [Header("Attacks")]
    [SerializeField] protected GameObject normalAttack;
    [SerializeField] protected GameObject secondaryAttack;

    protected int Health = 100;

    protected Rigidbody2D rb;
    protected SpriteRenderer spriteRenderer;
    protected bool facingRight = true;

    protected virtual void Awake()
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

    protected virtual void Start()
    {
        Health = 100;
    }

    public virtual void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Vector3 targetVelocity = new Vector3(input.x * hSpeed, input.y * vSpeed);

        rb.linearVelocity = targetVelocity;

        if (rb.linearVelocityX > 0 && !facingRight)
            Flip();
        else if (rb.linearVelocityX < 0 && facingRight)
            Flip();
    }

    public virtual void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping)
        {
            isJumping = true;
            jumpVelocity = jumpForce;
        }
    }

    protected virtual void HandleJump()
    {
        if (!isJumping) return;

        jumpVelocity -= gravity * Time.deltaTime;
        jumpHeight += jumpVelocity * Time.deltaTime;

        if (jumpHeight <= 0f)
        {
            jumpHeight = 0f;
            isJumping = false;
        }

        playerObject.localPosition = new Vector3(0f, jumpHeight, 0f);

        if (shadow != null)
        {
            float scale = Mathf.Lerp(1f, 0.4f, jumpHeight / jumpForce);
            shadow.localScale = new Vector3(scale, scale, 1f);
        }
    }

    public abstract void NormalAttack(InputAction.CallbackContext context);
    public abstract void SecondaryAttack(InputAction.CallbackContext context);

    protected virtual void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight = !facingRight;
    }
}
