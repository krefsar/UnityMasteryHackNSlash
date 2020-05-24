using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : PooledMonoBehaviour, ITakeHit, IDie
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Attacker attacker;

    [SerializeField]
    private PooledMonoBehaviour impactParticle;

    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;
    private Character target;

    public event Action<IDie> OnDied = delegate { };
    public event Action<int, int> OnHealthChanged = delegate { };

    private bool isDead {  get { return currentHealth <= 0; } }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (target == null)
        {
            AcquireTarget();
        }
        else
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance > 2f)
            {
                FollowTarget();
            }
            else
            {
                TryAttack();
            }
        }
    }

    private void AcquireTarget()
    {
        target = Character.AllCharacters
            .OrderBy(character => Vector3.Distance(transform.position, character.transform.position))
            .FirstOrDefault();

        animator.SetFloat("Speed", 0f);
    }

    private void FollowTarget()
    {
        animator.SetFloat("Speed", 1f);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void TryAttack()
    {
        animator.SetFloat("Speed", 0f);
        navMeshAgent.isStopped = true;

        if (attacker.CanAttack)
        {
            animator.SetTrigger("Attack");
            attacker.Attack(target);
        }
    }

    public void TakeHit(IAttack attacker)
    {
        currentHealth--;

        OnHealthChanged(currentHealth, maxHealth);

        impactParticle.Get<PooledMonoBehaviour>(transform.position + new Vector3(0, 2, 0), Quaternion.identity);

        if (isDead)
        {
            Die();
        } else
        {
            animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        navMeshAgent.isStopped = true;

        OnDied(this);

        ReturnToPool(6f);
    }
}
