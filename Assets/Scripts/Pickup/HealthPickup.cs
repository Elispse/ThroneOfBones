using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IHealable healable) && collision.gameObject.CompareTag("Player"))
        {
            healable.Heal(health);
            Debug.Log("Player grabbed pickup");
            Destroy(gameObject);
        }
    }
}
