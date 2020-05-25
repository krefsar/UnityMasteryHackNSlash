using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PooledMonoBehaviour CharacterPrefab;
    public Controller Controller;

    public bool HasController { get { return Controller != null; } }
    public int PlayerNumber { get { return playerNumber; } }

    public event Action<Character> OnCharacterChanged = delegate { };

    [SerializeField]
    private int playerNumber;
    [SerializeField]
    private float respawnTime = 5f;

    private UIPlayerText uiPlayerText;

    private void Awake()
    {
        uiPlayerText = GetComponentInChildren<UIPlayerText>();
    }

    public void InitializePlayer(Controller controller)
    {
        Controller = controller;

        gameObject.name = string.Format("Player {0} - {1}", playerNumber, controller.gameObject.name);

        uiPlayerText.HandlePlayerInitialized();
    }

    public void SpawnCharacter()
    {
        var character = CharacterPrefab.Get<Character>(Vector3.zero, Quaternion.identity);
        character.SetController(Controller);
        character.OnDied += HandleCharacterDied;

        OnCharacterChanged(character);
    }

    private void HandleCharacterDied(IDie character)
    {
        character.OnDied -= HandleCharacterDied;

        character.gameObject.SetActive(false);

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);
        SpawnCharacter();
    }
}
