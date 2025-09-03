using FMOD.Studio;
using UnityEngine;

public class KnightSecondary : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool nullifyCompletely = false;

    private EventInstance shield;
    private EventInstance shieldBlocked;

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

    private void Awake()
    {
        shield = AudioManager.instance.CreateInstance(FMODEvents.instance.knightShield);
        shieldBlocked = AudioManager.instance.CreateInstance(FMODEvents.instance.knightBlock);
        shield.start();
    }
}
