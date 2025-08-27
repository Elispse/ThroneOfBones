using UnityEngine;

public class KnightNormal : IAttack
{
    public override float damage => 33.34f;

    public override float knockback => 10f;

    public override string ignoreTag => "Player";
    public override float destroyTime => 0.1f;

    public override bool blocked { get; set; } = false;

    private void Awake()
    {
        
    }
}
