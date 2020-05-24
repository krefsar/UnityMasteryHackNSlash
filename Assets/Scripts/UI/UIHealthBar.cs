using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;

    private Character currentCharacter;

    private void Awake()
    {
        var player = GetComponentInParent<Player>();
        player.OnCharacterChanged += Player_OnCharacterChanged;
        gameObject.SetActive(false);
    }

    private void Player_OnCharacterChanged(Character character)
    {
        currentCharacter = character;
        currentCharacter.OnHealthChanged += HandleHealthChanged;
        currentCharacter.OnDied += HandleCharacterDied;
        gameObject.SetActive(true);
    }

    private void HandleCharacterDied(Character obj)
    {
        gameObject.SetActive(false);
    }

    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        float pct = (float)currentHealth / (float)maxHealth;
        foregroundImage.fillAmount = pct;
    }
}
