using System;
using UnityEngine;

public interface ITakeHit
{
    Transform transform { get; }
    event Action OnHit;
    bool Alive { get; }

    void TakeHit(IDamage attacker);
}
