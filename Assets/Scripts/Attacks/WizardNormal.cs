using UnityEngine;

public class WizardNormal : IAttack
{
    public override float damage =>  25.01f;

    public override float knockback => 5f;

    public override string ignoreTag => "Player";

    public override float destroyTime => 1f;

    public override bool blocked { get; set; } = false;

    private void Awake()
    {
        
    }
}
