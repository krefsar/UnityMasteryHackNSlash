using UnityEngine;

public class DeathParticles : MonoBehaviour
{
    [SerializeField]
    private PooledMonoBehaviour deathParticlePrefab;

    private IDie entity;

    private void Awake()
    {
        entity = GetComponent<IDie>();
    }

    private void OnEnable()
    {
        entity.OnDied += HandleCharacterDeath;
    }

    private void OnDisable()
    {
        entity.OnDied -= HandleCharacterDeath;
    }

    private void HandleCharacterDeath(IDie entity)
    {
        entity.OnDied -= HandleCharacterDeath;

        Vector3 spawnPosition = transform.position + new Vector3(0, 2f, 0);
        deathParticlePrefab.Get<PooledMonoBehaviour>(spawnPosition, Quaternion.identity);
    }
}
