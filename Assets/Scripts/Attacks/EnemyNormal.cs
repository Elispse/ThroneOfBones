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

    void Update()
    {
        if (projectile)
        {
            transform.Translate(Vector3.right * (facingRight? 1 : -1 )* 0.5f);
        }
    }
}
