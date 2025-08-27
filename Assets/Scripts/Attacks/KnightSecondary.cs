using UnityEngine;

public class KnightSecondary : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float damageReduction = 35f;
    [SerializeField] private bool nullifyCompletely = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IAttack attack = collision.GetComponent<IAttack>();
        if (attack != null)
        {
            if (nullifyCompletely)
            {
                attack.blocked = true;
                Debug.Log("Attack completely blocked!");
            }
        }
    }
}
