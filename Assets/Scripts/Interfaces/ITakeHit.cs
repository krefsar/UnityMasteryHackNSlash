using System;

public interface ITakeHit
{
    event Action OnHit;

    void TakeHit(IDamage attacker);
}
