using UnityEngine;

[RequireComponent(typeof(Character))]
public class DeathParticles : MonoBehaviour
{
    [SerializeField]
    private PooledMonoBehaviour deathParticlePrefab;

    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void OnEnable()
    {
        character.OnDied += HandleCharacterDeath;
    }

    private void OnDisable()
    {
        character.OnDied -= HandleCharacterDeath;
    }

    private void HandleCharacterDeath(Character character)
    {
        character.OnDied -= HandleCharacterDeath;

        Vector3 spawnPosition = character.transform.position + new Vector3(0, 2f, 0);
        deathParticlePrefab.Get<PooledMonoBehaviour>(spawnPosition, Quaternion.identity);
    }
}
