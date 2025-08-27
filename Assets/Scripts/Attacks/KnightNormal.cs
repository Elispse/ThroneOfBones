using FMOD.Studio;
using UnityEngine;

public class KnightNormal : IAttack
{
    public override float damage => 33.34f;

    public override float knockback => 10f;

    public override string ignoreTag => "Player";
    public override float destroyTime => 0.1f;

    public override bool blocked { get; set; } = false;
    private EventInstance attack;

    protected override void Start()
    {
        Destroy(gameObject, destroyTime);
        attack = AudioManager.instance.CreateInstance(FMODEvents.instance.knightSwordSwing);
    }

    private void Awake()
    {
        attack.start();
    }
}
