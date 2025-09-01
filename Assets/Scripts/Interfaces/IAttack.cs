using UnityEngine;
public abstract class IAttack : MonoBehaviour
{
    public abstract float damage { get; }
    public abstract float knockback { get; }
    public abstract string ignoreTag { get; }
    public abstract float destroyTime { get; }
    public abstract bool blocked { get; set; }

    public abstract IPlayer owner { get; set; }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable);
        if (damagable != null && !blocked && !collision.CompareTag(ignoreTag))
        {
            damagable.ApplyDamage(damage);
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            damagable.Knockback(direction, knockback);
            if (owner) owner.addCombo();
        }
    }
}
