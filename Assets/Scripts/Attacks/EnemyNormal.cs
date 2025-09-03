using FMOD.Studio;
using UnityEngine;

public class EnemyNormal : IAttack
{
    public override float damage => 20f;
    public override float knockback => 5f;
    public override string ignoreTag => "Enemy";
    public override float destroyTime => 0.1f;

    public bool projectile = false;
    public override bool facingRight { get; set; } = true;
    public override bool blocked { get; set; } = false;
    public override IPlayer owner { get; set; }

    [SerializeField]
    private EnemyType enemyType;

    private EventInstance sound;

    void Update()
    {
        if (projectile)
        {
            transform.Translate(Vector3.right * (facingRight? 1 : -1 )* 0.5f);
        }
    }

    private void Awake()
    {
        switch(enemyType)
        { 
            case EnemyType.EVILKNIGHT:
                sound = AudioManager.instance.CreateInstance(FMODEvents.instance.knightSwordSwing);
                sound.start();
                break;
            case EnemyType.EYEBAT:
                sound = AudioManager.instance.CreateInstance(FMODEvents.instance.EyebatAttack);
                sound.start();
                break;
            case EnemyType.FREAK:
                sound = AudioManager.instance.CreateInstance(FMODEvents.instance.FreakAttack);
                sound.start();
                break;
            case EnemyType.GOBLIN:
                sound = AudioManager.instance.CreateInstance(FMODEvents.instance.GoblinAttack);
                sound.start();
                break;
            case EnemyType.NECROMANCER:
                sound = AudioManager.instance.CreateInstance(FMODEvents.instance.NecromancerAttack);
                sound.start();
                break;
            case EnemyType.SKELETONKNIGHT:
                sound = AudioManager.instance.CreateInstance(FMODEvents.instance.SkeletonKnightAttack);
                sound.start();
                break;
            case EnemyType.SKELETONWIZARD:
                sound = AudioManager.instance.CreateInstance(FMODEvents.instance.SkeletonWizardAttack);
                sound.start();
                break;
        }
    }
}
