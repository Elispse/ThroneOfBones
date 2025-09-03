using FMOD.Studio;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

enum EnemyType
{
    EVILKNIGHT,
    EYEBAT,
    FREAK,
    GOBLIN,
    NECROMANCER,
    SKELETONKNIGHT,
    SKELETONWIZARD
}

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject normalAttack;
    [SerializeField] int AIType = 0; // 0 is random, 1 is chase player, 2 is flee, 3 is rush

    private Rigidbody2D rb;
    private int Health = 100;
    private int attackTimer = 0;
    [SerializeField] public int AttackDelay = 90;

    [SerializeField] public float attackTime = 0.5f;
    [SerializeField] public float fleeDistance = 20f;
    private bool facingRight = true;
    private bool isAttacking = false;
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

    [SerializeField]
    private EnemyType enemyType;

    private EventInstance deathSound;
    private EventInstance hurtSound;

    public void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        switch (enemyType)
        {
            case EnemyType.EVILKNIGHT:
                deathSound = AudioManager.instance.CreateInstance(FMODEvents.instance.EvilKnightDeath);
                hurtSound = AudioManager.instance.CreateInstance(FMODEvents.instance.EvilKnightHit);
                break;
            case EnemyType.EYEBAT:
                deathSound = AudioManager.instance.CreateInstance(FMODEvents.instance.EyebatDeath);
                hurtSound = AudioManager.instance.CreateInstance(FMODEvents.instance.EyebatHit);
                break;
            case EnemyType.FREAK:
                deathSound = AudioManager.instance.CreateInstance(FMODEvents.instance.FreakDeath);
                hurtSound = AudioManager.instance.CreateInstance(FMODEvents.instance.FreakHit);
                break;
            case EnemyType.GOBLIN:
                deathSound = AudioManager.instance.CreateInstance(FMODEvents.instance.GoblinDeath);
                hurtSound = AudioManager.instance.CreateInstance(FMODEvents.instance.GoblinHit);
                break;
            case EnemyType.NECROMANCER:
                deathSound = AudioManager.instance.CreateInstance(FMODEvents.instance.NecromancerDeath);
                hurtSound = AudioManager.instance.CreateInstance(FMODEvents.instance.NecromancerHit);
                break;
            case EnemyType.SKELETONKNIGHT:
                deathSound = AudioManager.instance.CreateInstance(FMODEvents.instance.SkeletonKnightDeath);
                hurtSound = AudioManager.instance.CreateInstance(FMODEvents.instance.SkeletonKnightHit);
                break;
            case EnemyType.SKELETONWIZARD:
                deathSound = AudioManager.instance.CreateInstance(FMODEvents.instance.SkeletonWizardDeath);
                hurtSound = AudioManager.instance.CreateInstance(FMODEvents.instance.SkeletonWizardHit);
                break;
        }
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
        if (isJumping || isAttacking) return;
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
            case 3:
                rush();
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
        animator.SetBool("Move", movement.magnitude > 0.01);
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
        //enemyObject.localPosition = new Vector3(facingRight? originalXPos : originalXPos - flipOffset, enemyObject.localPosition.y, enemyObject.localPosition.z);
    }

    public void ApplyDamage(float damage)
    {
        Health -= (int)damage;
        if (Health <= 0)
        {
            deathSound.start();
            animator.SetTrigger("Death");
            Destroy(gameObject, 0.2f);
            GameManager.Instance.AddScore(10);
        }
        else
        {
            animator.SetTrigger("Hurt");
            hurtSound.start();
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

        jumpVelocity -= gravity * Time.deltaTime;
        jumpHeight += jumpVelocity * Time.deltaTime;


        if (jumpHeight <= 0f)
        {
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
            if (minDistance > 2f)
            {
                Move(direction, 3f);
            }
            if (minDistance <= 3f)
            {
                NormalAttack();
            }
        }
    }
    private IEnumerator FleeAndAttack(float targetFleeDistance)
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

        float currentDistance = Vector2.Distance(transform.position, nearestPlayer.transform.position);
        while (currentDistance < targetFleeDistance)
        {
            Vector2 fleeDirection = (transform.position - nearestPlayer.transform.position).normalized;
            Move(fleeDirection, 1.5f);
            currentDistance = Vector2.Distance(transform.position, nearestPlayer.transform.position);
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
        StartCoroutine(FleeAndAttack(fleeDistance));
    }

    public void rush()
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
            Move(direction, 5f);
        }
        if (minDistance <= 2f)
        {
            NormalAttack();
        }
    }
    public void NormalAttack()
    {
        if (attackTimer >= AttackDelay && !isAttacking)
        {
            attackTimer = 0;
            StartCoroutine(DelayedNormalAttack(attackTime));
        }
    }
    private IEnumerator DelayedNormalAttack(float delay)
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(delay);
        var attack = Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
        attack.GetComponent<IAttack>().facingRight = facingRight;
        isAttacking = false;
    }
}
