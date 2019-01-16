using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(int value);
    float GetHealthRatio();
    Vector3 GetPosition();
}
