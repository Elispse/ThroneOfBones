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
            var attack = Instantiate(normalAttack, transform.position + new Vector3(facingRight ? 1 : -1, 0, 0), Quaternion.identity);
            attack.GetComponent<Rigidbody2D>().AddForce((new Vector3(facingRight ? 1 : -1, 0, 0) * 5.0f), ForceMode2D.Impulse);
        }
    }

    public override void SecondaryAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
