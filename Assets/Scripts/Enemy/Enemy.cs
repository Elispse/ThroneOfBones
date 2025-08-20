using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private Rigidbody2D rb;
    private int Health = 100;

    private Vector2 movement = Vector2.zero;

    public void ApplyDamage(float damage)
    {
        Health -= (int)damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
