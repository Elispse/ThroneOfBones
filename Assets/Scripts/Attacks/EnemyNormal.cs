using UnityEngine;

public class KnightNormal : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable);
        if (damagable != null)
        {
            damagable.ApplyDamage(101f);
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            damagable.Knockback(direction, 1);
        }
    }
}
