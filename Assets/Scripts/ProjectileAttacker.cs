using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttacker : MonoBehaviour, IAttack
{
    public int Damage { get { return 1; } }
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    [SerializeField]
    private float attackRefreshSpeed = 1f;
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private float launchYOffset = 1f;

    private Animator animator;

    private float attackTimer;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    public void Attack()
    {
        attackTimer = 0;

        animator.SetTrigger("Attack");

        Vector3 spawnPosition = transform.position + Vector3.up * launchYOffset;
        projectilePrefab.Get<PooledMonoBehaviour>(spawnPosition, transform.rotation);
    }
}
