using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightPlayer : IPlayer
{
    private GameObject shield;
    public override void Update()
    {
        HandleJump();
        rb.linearVelocity = targetVelocity;
        if (shield)
        {
            shield.transform.position = transform.position + new Vector3(facingRight ? 1 : -1, 0, 0);
        }   
    }

    public override void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.performed && normalCDComplete)
        {
            var attack = Instantiate(normalAttack, playerObject.transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
            animator.SetTrigger("Primary");
            animator.SetBool("First", !animator.GetBool("First"));
            attack.GetComponent<IAttack>().owner = this;
            StartCoroutine(NormalDelay(normalAttackCooldown));
        }
    }

    public override void SecondaryAttack(InputAction.CallbackContext context)
    {
        if (!shield && secondaryCDComplete)
        {
            Debug.Log("Secondary attack performed.");
            shield = Instantiate(secondaryAttack, playerObject.transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
            animator.SetBool("Shield", true);
        }
        if (context.canceled)
        {
            if (shield)
            {
                animator.SetBool("Shield", false);
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
