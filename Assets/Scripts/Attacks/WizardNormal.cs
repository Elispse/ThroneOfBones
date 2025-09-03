using FMOD.Studio;
using UnityEngine;

public class WizardNormal : IAttack
{
    public override float damage =>  25.01f;

    public override float knockback => 5f;

    public override string ignoreTag => "Player";

    public override float destroyTime => 1f;

    public override bool blocked { get; set; } = false;
    public override IPlayer owner { get; set; }
    public override bool facingRight { get; set; } = true;
    private EventInstance attack;

    private void Awake()
    {
        attack = AudioManager.instance.CreateInstance(FMODEvents.instance.wizardPrimary);
        attack.start();
    }
}
