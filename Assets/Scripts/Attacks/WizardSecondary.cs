using FMOD.Studio;
using UnityEngine;

public class WizardSecondary : IAttack
{
    public override float damage => 5.0f;

    public override float knockback => 15f;

    public override string ignoreTag => "Player";

    public override float destroyTime => 1f;

    public override bool blocked { get; set; } = false;
    public override IPlayer owner { get; set; }
    public override bool facingRight { get; set; } = true;
    private EventInstance attack;

    private void Awake()
    {
        attack = AudioManager.instance.CreateInstance(FMODEvents.instance.wizardSecondary);
        attack.start();
    }
}
