using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelectionMarker : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Image markerImage;
    [SerializeField]
    private Image lockImage;

    private UICharacterSelectionMenu menu;
    private bool initializing;
    private bool initialized;

    public bool IsLockedIn { get; private set; }
    public bool IsPlayerIn { get { return player.HasController; } }

    private void Awake()
    {
        menu = GetComponentInParent<UICharacterSelectionMenu>();
        markerImage.gameObject.SetActive(false);
        lockImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsPlayerIn == false)
        {
            return;
        }

        if (!initializing)
        {
            StartCoroutine(Initialize());
        }

        if (!initialized)
        {
            return;
        }

        // Check for player controls and selections + locking character
        if (!IsLockedIn)
        {
            if (player.Controller.horizontal > 0.5)
            {
                MoveToCharacterPanel(menu.RightPanel);
            }
            else if (player.Controller.horizontal < -0.5)
            {
                MoveToCharacterPanel(menu.LeftPanel);
            }

            if (player.Controller.attackPressed)
            {
                StartCoroutine(LockCharacter());
            }
        } else
        {
            if (player.Controller.attackPressed)
            {
                menu.TryStartGame();
            }
        }
    }

    private IEnumerator LockCharacter()
    {
        lockImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        IsLockedIn = true;
    }

    private void MoveToCharacterPanel(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
        player.CharacterPrefab = panel.CharacterPrefab;
    }

    private IEnumerator Initialize()
    {
        initializing = true;
        MoveToCharacterPanel(menu.LeftPanel);
        yield return new WaitForSeconds(0.5f);

        markerImage.gameObject.SetActive(true);
        initialized = true;
    }
}
