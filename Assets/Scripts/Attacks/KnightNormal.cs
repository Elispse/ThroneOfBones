using UnityEngine;

public class KnightNormal : MonoBehaviour, IAttack
{
    [Header("Settings")]
    public int damage { get; set; } = 35;
    public bool blocked { get; set; } = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) return;
        collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable);
        if (damagable != null !& blocked)
        {
            damagable.ApplyDamage(damage);
        }
    }
}
