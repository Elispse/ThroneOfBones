using UnityEngine;

public class WizardSecondary : IAttack
{
    public override float damage => 5.0f;

    public override float knockback => 30.0f;

    public override string ignoreTag => "Player";

    public override float destroyTime => 1f;

    public override bool blocked { get; set; } = false;
}
