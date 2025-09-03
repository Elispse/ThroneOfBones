using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public abstract class IPlayer : MonoBehaviour, IDamagable
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

    [Header("Attack Settings")]
    [SerializeField] protected float normalAttackCooldown = 0.2f;
    [SerializeField] protected float secondaryAttackCooldown = 0.5f;
    protected bool normalCDComplete = true;
    protected bool secondaryCDComplete = true;

    [SerializeField] public Animator animator;

    public int Health = 100;

    public int Combo = 0;

    protected Rigidbody2D rb;
    [SerializeField] SpriteRenderer spriteRenderer;
    public bool facingRight = true;
    protected Vector3 targetVelocity = Vector3.zero;

    private UIDocument HUD;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        rb.linearDamping = 1f;
    }

    public virtual void Start()
    {
        Health = 100;
    
        HUD = GameObject.FindAnyObjectByType<UIDocument>();
    }

    public virtual void Update()
    {
        HandleJump();
        rb.linearVelocity = targetVelocity;

    }

    public virtual void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        targetVelocity = new Vector3(input.x * hSpeed, input.y * vSpeed);

        rb.linearVelocity = targetVelocity;
        if (targetVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (targetVelocity.x < 0 && facingRight)
        {
            Flip();
        }

        if (targetVelocity.magnitude > 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
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

        animator.SetBool("Airborne", true);
        jumpVelocity -= gravity * Time.deltaTime;
        jumpHeight += jumpVelocity * Time.deltaTime;


        if (jumpHeight <= 0f)
        {
            animator.SetBool("Airborne", false);

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
        playerObject.localScale = new Vector3(-playerObject.localScale.x, playerObject.localScale.y, playerObject.localScale.z);
        facingRight = !facingRight;
    }

    public virtual void ApplyDamage(float damage)
    {
        Health -= (int)damage;
        if (Health <= 0) GameManager.Instance.Death();
    }

    public virtual void Knockback(Vector2 direction, float force)
    {
        rb.linearVelocity = (direction.normalized * force);
    }

    public virtual void addCombo()
    {
        Combo++;
    }

    public virtual void OpenSettings()
    {
        GameManager.Instance.Pause();
        HUD.GetComponent<HUDscript>().Settings();
    }

}
