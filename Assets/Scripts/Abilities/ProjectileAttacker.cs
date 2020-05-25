using System.Collections;
using UnityEngine;

public class ProjectileAttacker : AbilityBase, IAttack
{
    public int Damage { get { return damage; } }

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private float launchYOffset = 1f;
    [SerializeField]
    private float launchDelay = 1f;
    [SerializeField]
    private int damage = 1;

    public void Attack()
    {
        attackTimer = 0;

        StartCoroutine(LaunchAfterDelay());
    }

    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(launchDelay);
        Vector3 spawnPosition = transform.position + Vector3.up * launchYOffset;
        projectilePrefab.Get<PooledMonoBehaviour>(spawnPosition, transform.rotation);
    }

    protected override void OnUse()
    {
        Attack();
    }
}
