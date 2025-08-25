using UnityEngine;

public class EnemyNormal : IAttack
{
    public override float damage => 20f;
    public override float knockback => 8f;
    public override string ignoreTag => "Enemy";
    public override float destroyTime => 0.1f;
    void Update()
    {
        
    }
}
