using UnityEngine;

public class EnemyNormal : IAttack
{
    public override float damage => 20f;
    public override float knockback => 125f;
    public override string ignoreTag => "Enemy";
    public override float destroyTime => 0.1f;

    public override bool blocked { get; set; } = false;
    public override IPlayer owner { get; set; }

    void Update()
    {
        
    }
}
