using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void ApplyDamage(float damage);
    void Knockback(Vector2 direction, float force);
}