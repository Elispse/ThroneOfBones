using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnightPlayer : IPlayer
{
    private GameObject shield;
    private bool shieldActive = false;
    public override void Update()
    {
        HandleJump();
        rb.linearVelocity = targetVelocity;
        if (shield)
        {
            shield.transform.position = transform.position + new Vector3(facingRight ? 1 : -1, 0, 0);
        }   
    }

    public override void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        targetVelocity = new Vector3(input.x * hSpeed, input.y * vSpeed);

        rb.linearVelocity = targetVelocity * (shieldActive? 0.25f : 1f);
        if (targetVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (targetVelocity.x < 0 && facingRight)
        {
            Flip();
        }

        if (targetVelocity.magnitude > 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
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
            shieldActive = true;
        }
        if (context.canceled)
        {
            if (shield)
            {
                animator.SetBool("Shield", false);
                Destroy(shield);
                shield = null;
                shieldActive = false;
                StartCoroutine(SecondaryDelay(secondaryAttackCooldown));
            }
        }
    }
    public override void ApplyDamage(float damage)
    {
        if(shieldActive) return;
        Health -= (int)damage;
        if (Health <= 0) GameManager.Instance.Death();
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
