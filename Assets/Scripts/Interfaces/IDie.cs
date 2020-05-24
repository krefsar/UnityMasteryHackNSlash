using System;
using UnityEngine;

public interface IDie
{
    event Action<IDie> OnDied;
    event Action<int, int> OnHealthChanged;

    GameObject gameObject { get; }
}
