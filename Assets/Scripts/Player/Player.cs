using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour, IDamagable
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
    [SerializeField] private Transform playerObject;
    [SerializeField] private Transform shadow;

    [Header("Attacks")]
    [SerializeField] GameObject normalAttack;
    [SerializeField] GameObject secondaryAttack;

    private int Health = 100;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    private GameObject shield = null;
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
        if (shield)
        {
            shield.transform.position = transform.position + new Vector3(facingRight ? 1 : -1, 0, 0);
        }
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

    public void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
        }
    }

    public void SecondaryAttack(InputAction.CallbackContext context)
    {
        if (!shield)
        {
            Debug.Log("Secondary attack performed.");
            shield = Instantiate(secondaryAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
        }
        
        if (context.canceled)
        {
            Destroy(shield);
            shield = null;
        }
    }


    public void ApplyDamage(float damage)
    {
        Health -= (int)damage;
    }

    private void Flip()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight = !facingRight;
    }



}
