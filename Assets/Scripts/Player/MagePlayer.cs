using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagePlayer : IPlayer
{
    private void Update()
    {
        HandleJump();
    }

    public override void NormalAttack(InputAction.CallbackContext context)
    {
        if (context.performed && normalCDComplete)
        {
            animator.SetTrigger("Primary");
            var attack = Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
            attack.GetComponent<Rigidbody2D>().AddForce((new Vector3(facingRight ? 1 : -1, 0, 0) * 5.0f), ForceMode2D.Impulse);
            StartCoroutine(NormalDelay(normalAttackCooldown));
        }
    }

    public override void SecondaryAttack(InputAction.CallbackContext context)
    {
        if (context.performed && secondaryCDComplete)
        {
            animator.SetTrigger("Secondary");
            Debug.Log("Secondary attack performed.");
            Instantiate(secondaryAttack, transform.position + new Vector3(facingRight ? 2.5f : -2.5f, 0), Quaternion.Euler(0, 0,facingRight ? 90 : 270));
            StartCoroutine(SecondaryDelay(secondaryAttackCooldown));
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
