using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject normalAttack;

    private Rigidbody2D rb;
    private int Health = 100;
    private float moveTimer = 0;
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
        moveTimer += Time.deltaTime;
        if (moveTimer >= 2.5f)
        {
            moveTimer = 0f;
            randomMove();
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
    public void NormalAttack()
    {
        Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
    }
}
