using UnityEngine;

public class UICharacterSelectionPanel : MonoBehaviour
{
    public Character CharacterPrefab { get { return characterPrefab; } }

    [SerializeField]
    private Character characterPrefab;
}
