using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightPlayer : IPlayer, IDamagable
{
    private GameObject shield;
    private void Update()
    {
        HandleJump();
    }

    public override void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.performed && normalCDComplete)
        {
            Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
            StartCoroutine(AttackDelay(normalAttackCooldown));
        }
    }

    public override void SecondaryAttack(InputAction.CallbackContext context)
    {
        if (!shield)
        {
            Debug.Log("Secondary attack performed.");
            shield = Instantiate(secondaryAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
        }
        if (context.canceled)
        {
            if (shield)
            {
                Destroy(shield);
                shield = null;
            }
        }
    }

    private IEnumerator AttackDelay(float attackCooldown)
    {
        normalCDComplete = false;
        yield return new WaitForSeconds(attackCooldown);
        normalCDComplete = true;
    }
}
