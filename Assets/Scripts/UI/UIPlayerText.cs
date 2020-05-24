using System.Collections;
using TMPro;
using UnityEngine;

public class UIPlayerText : MonoBehaviour
{
    private TextMeshProUGUI tmText;

    private void Awake()
    {
        tmText = GetComponent<TextMeshProUGUI>();
    }

    internal void HandlePlayerInitialized()
    {
        tmText.text = "Player Joined!";
        StartCoroutine(ClearTextAfterDelay());
    }

    private IEnumerator ClearTextAfterDelay()
    {
        yield return new WaitForSeconds(2);
        tmText.text = string.Empty;
    }
}
