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
        player.OnCharacterChanged += HandleCharacterChanged;
        gameObject.SetActive(false);
    }

    private void HandleCharacterChanged(Character character)
    {
        currentCharacter = character;
        currentCharacter.OnHealthChanged += HandleHealthChanged;
        currentCharacter.OnDied += HandleCharacterDied;
        gameObject.SetActive(true);
    }

    private void HandleCharacterDied(IDie character)
    {
        character.OnHealthChanged -= HandleHealthChanged;
        character.OnDied -= HandleCharacterDied;

        currentCharacter = null;
        gameObject.SetActive(false);
    }

    private void HandleHealthChanged(int currentHealth, int maxHealth)
    {
        float pct = (float)currentHealth / (float)maxHealth;
        foregroundImage.fillAmount = pct;
    }
}
