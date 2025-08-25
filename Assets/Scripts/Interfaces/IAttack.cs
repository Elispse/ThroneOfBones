using UnityEngine;

public interface IAttack
{
    int damage
    {
        get;
        set;
    }

    bool blocked
    {
        get;
        set;
    }
}
