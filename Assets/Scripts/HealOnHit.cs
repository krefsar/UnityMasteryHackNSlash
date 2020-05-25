using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOnHit : MonoBehaviour, ITakeHit
{
    public bool Alive { get { return true; } }

    public event Action OnHit = delegate { };

    [SerializeField]
    private int healAmount;
    [SerializeField]
    private bool disableOnUse = true;

    public void TakeHit(IDamage attacker)
    {
        var character = attacker.transform.GetComponent<Character>();
        if (character != null)
        {
            OnHit();
            character.Heal(healAmount);

            if (disableOnUse)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
