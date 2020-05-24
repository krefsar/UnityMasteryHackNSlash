using UnityEngine;

public interface IAttack
{
    Transform transform { get; }
    int Damage { get; }
}