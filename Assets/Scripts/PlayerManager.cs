using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private Player[] players;

    private void Awake()
    {
        players = FindObjectsOfType<Player>();
        Instance = this;
    }

    public void AddPlayerToGame(Controller controller)
    {
        var firstNonActivePlayer = players
            .OrderBy(player => player.PlayerNumber)
            .FirstOrDefault(player => player.HasController == false);

        firstNonActivePlayer.InitializePlayer(controller);
    }

    public void SpawnPlayerCharacters()
    {
        foreach (var player in players)
        {
            if (player.HasController && player.CharacterPrefab != null)
            {
                player.SpawnCharacter();
            }
        }
    }
}
