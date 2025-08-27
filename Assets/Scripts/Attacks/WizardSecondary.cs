using UnityEngine;

public class WizardSecondary : IAttack
{
    public override float damage => 5.0f;

    public override float knockback => 10.0f;

    public override string ignoreTag => "Player";

    public override float destroyTime => 0.25f;

    public override bool blocked { get; set; } = false;
}
