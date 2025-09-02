using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject normalAttack;
    [SerializeField] int AIType = 0; // 0 is random, 1 is chase player, 2 is flee

    private Rigidbody2D rb;
    private int Health = 100;
    private int attackTimer = 0;
    [SerializeField] public int AttackDelay = 90;

    [SerializeField] public float attackTime = 0.5f;
    private bool facingRight = true;
    [SerializeField] public Animator animator;

    [SerializeField] public float flipOffset = 0;
    private float originalXPos;

    private Vector2 movement = Vector2.zero;
    [SerializeField] public SpriteRenderer spriteRenderer;


    [Header("Jump Settings")]
    [SerializeField] protected float jumpForce = 1f;
    [SerializeField] protected float gravity = 20f;
    protected float jumpHeight = 0f;
    protected float jumpVelocity = 0f;
    protected bool isJumping = false;


    [SerializeField] public Transform enemyObject;
    [SerializeField] public Transform shadow;

    public void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer not found on Player.");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 1f;
        originalXPos = enemyObject.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();
        rb.linearVelocity *= 0.9f;
        attackTimer++;
        if (isJumping) return;
        switch (AIType)
        {
            case 0:
                randomMove();
                break;
            case 1:
                runToNearestPlayer();
                break;
            case 2:
                flee();
                break;
            default:
                randomMove();
                break;
        }
    }

    public void Move(Vector2 direction, float speed)
    {
        movement = direction.normalized * speed;
        rb.linearVelocity = movement;
        animator.SetBool("Move", movement.magnitude > 0);
        if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (movement.x < 0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        enemyObject.localScale = new Vector3(-enemyObject.localScale.x, enemyObject.localScale.y, enemyObject.localScale.z);
        facingRight = !facingRight;
        enemyObject.localPosition = new Vector3(facingRight? originalXPos : originalXPos - flipOffset, enemyObject.localPosition.y, enemyObject.localPosition.z);
    }

    public void ApplyDamage(float damage)
    {
        Health -= (int)damage;
        if (Health <= 0)
        {
            animator.SetTrigger("Death");
            Destroy(gameObject, 0.2f);
            GameManager.Instance.AddScore(10);
        }
        else animator.SetTrigger("Hurt");
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

        enemyObject.localPosition = new Vector3(0f, jumpHeight, 0f);

        if (shadow != null)
        {
            float scale = Mathf.Lerp(1f, 0.4f, jumpHeight / jumpForce);
            shadow.localScale = new Vector3(scale, scale, 1f);
        }
    }
    public void Knockback(Vector2 direction, float force)
    {
        rb.linearVelocity = (direction.normalized * force);
        if (!isJumping)
        {
            isJumping = true;
            jumpVelocity = jumpForce;
        }

    }

    public void randomMove()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
        Move(randomDirection, 3f);
        if (attackTimer >= AttackDelay) NormalAttack();

    }

    public void runToNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) return;
        GameObject nearestPlayer = null;
        float minDistance = float.MaxValue;
        foreach (var player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPlayer = player;
            }
        }
        if (nearestPlayer != null)
        {
            Vector2 direction = (nearestPlayer.transform.position - transform.position).normalized;
            if(minDistance > 2f)
            {
                Move(direction, 3f);
            }
            if (minDistance <= 3f)
            {
                NormalAttack();
            }
        }
    }
    private IEnumerator FleeAndAttack(float fleeDuration)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) yield break;

        GameObject nearestPlayer = null;
        float minDistance = float.MaxValue;
        foreach (var player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPlayer = player;
            }
        }

        if (nearestPlayer == null) yield break;

        float timer = 0f;
        while (timer < fleeDuration)
        {
            Vector2 fleeDirection = (transform.position - nearestPlayer.transform.position).normalized;
            Move(fleeDirection, 1.5f);
            timer += Time.deltaTime;
            yield return null;
        }

        Vector2 toPlayer = nearestPlayer.transform.position - transform.position;
        if ((toPlayer.x > 0 && !facingRight) || (toPlayer.x < 0 && facingRight))
        {
            Flip();
        }

        NormalAttack();
    }
    public void flee()
    {
        StartCoroutine(FleeAndAttack(.2f));
    }
    public void NormalAttack()
    {
        if (attackTimer >= AttackDelay)
        {
            attackTimer = 0;
            StartCoroutine(DelayedNormalAttack(attackTime));
            animator.SetTrigger("Attack");
        }
    }
    private IEnumerator DelayedNormalAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
        animator.SetTrigger("Attack");
    }
}
