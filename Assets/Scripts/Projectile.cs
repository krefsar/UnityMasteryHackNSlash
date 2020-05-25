using System;
using UnityEngine;

public class Projectile : PooledMonoBehaviour, IDamage
{
    public int Damage { get { return 1; } }

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private PooledMonoBehaviour impactParticlePrefab;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.collider.GetComponent<ITakeHit>();
        if (hit != null)
        {
            Impact(hit);
        } else
        {
            impactParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);
            ReturnToPool();
        }
    }

    private void Impact(ITakeHit hit)
    {
        impactParticlePrefab.Get<PooledMonoBehaviour>(transform.position, Quaternion.identity);

        hit.TakeHit(this);

        ReturnToPool();
    }
}
