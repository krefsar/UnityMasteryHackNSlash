using UnityEngine;

public class ProjectileAttacker : AbilityBase, IAttack
{
    public int Damage { get { return 1; } }

    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private float launchYOffset = 1f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Attack()
    {
        attackTimer = 0;

        animator.SetTrigger("Attack");

        Vector3 spawnPosition = transform.position + Vector3.up * launchYOffset;
        projectilePrefab.Get<PooledMonoBehaviour>(spawnPosition, transform.rotation);
    }

    protected override void OnTryUse()
    {
        Attack();
    }
}
