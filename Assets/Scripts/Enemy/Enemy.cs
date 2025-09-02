using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject normalAttack;
    [SerializeField] int AIType = 0; // 0 is random, 1 is chase player, 2 is flee

    private Rigidbody2D rb;
    private int Health = 100;
    private int attackTimer = 0;
    public int AttackDelay = 60;
    private bool facingRight = true;

    private Vector2 movement = Vector2.zero;
    private SpriteRenderer spriteRenderer;
    public void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null)
            Debug.LogError("SpriteRenderer not found on Player.");
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity *= 0.9f;
        attackTimer++;
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
        spriteRenderer.flipX = !spriteRenderer.flipX;
        facingRight = !facingRight;
    }

    public void ApplyDamage(float damage)
    {
        Health -= (int)damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.AddScore(10);
        }
    }

    public void Knockback(Vector2 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    public void randomMove()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        Vector2 randomDirection = new Vector2(randomX, randomY).normalized;
        Move(randomDirection, 3f);
        NormalAttack();

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
            Move(direction, 5f);
            if (minDistance < 2f)
            {
                NormalAttack();
            }
        }
    }

    public void flee()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0) return;
        Vector2 fleeDirection = Vector2.zero;
        foreach (var player in players)
        {
            Vector2 direction = (transform.position - player.transform.position).normalized;
            fleeDirection += direction;
        }
        fleeDirection = fleeDirection.normalized;
        Move(fleeDirection, 3f);
    }
    public void NormalAttack()
    {
        Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
    }
}
