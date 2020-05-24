using UnityEngine;

public interface IAttack
{
    Transform transform { get; }
    int Damage { get; }
    bool CanAttack { get; }

    void Attack();
}