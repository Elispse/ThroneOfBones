using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightPlayer : IPlayer
{
    private GameObject shield;
    private void Update()
    {
        HandleJump();
        if (shield)
        {
            shield.transform.position = transform.position + new Vector3(facingRight ? 1 : -1, 0, 0);
        }
    }

    public override void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.performed && normalCDComplete)
        {
            var attack = Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
            attack.GetComponent<IAttack>().owner = this;
            StartCoroutine(NormalDelay(normalAttackCooldown));
        }
    }

    public override void SecondaryAttack(InputAction.CallbackContext context)
    {
        if (!shield && secondaryCDComplete)
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
                StartCoroutine(SecondaryDelay(secondaryAttackCooldown));
            }
        }
    }

    private IEnumerator NormalDelay(float attackCooldown)
    {
        normalCDComplete = false;
        yield return new WaitForSeconds(attackCooldown);
        normalCDComplete = true;
    }

    private IEnumerator SecondaryDelay(float attackCooldown)
    {
        secondaryCDComplete = false;
        yield return new WaitForSeconds(attackCooldown);
        secondaryCDComplete = true;
    }
}
