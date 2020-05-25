using UnityEngine;

public class ImpactParticles : MonoBehaviour
{
    [SerializeField]
    private PooledMonoBehaviour impactParticle;

    private ITakeHit entity;

    private void Awake()
    {
        entity = GetComponent<ITakeHit>();
    }

    private void OnEnable()
    {
        entity.OnHit += HandleHit;
    }

    private void HandleHit()
    {
        impactParticle.Get<PooledMonoBehaviour>(transform.position + new Vector3(0, 2, 0), Quaternion.identity);
    }
}
