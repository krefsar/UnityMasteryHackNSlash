using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : PooledMonoBehaviour, ITakeHit, IDie
{
    public bool Alive { get; private set; }

    public event Action<IDie> OnDied = delegate { };
    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action OnHit = delegate { };

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Attacker attacker;

    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;
    private Character target;

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
        Alive = true;
    }

    private void Update()
    {
        if (isDead)
        {
            return;
        }

        if (target == null || !target.Alive)
        {
            AcquireTarget();
        }
        else
        {
            if (attacker.InAttackRange(target) == false)
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
            attacker.Attack(target);
        }
    }

    public void TakeHit(IDamage attacker)
    {
        currentHealth -= attacker.Damage;

        OnHealthChanged(currentHealth, maxHealth);

        OnHit();

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

        Alive = false;
        OnDied(this);

        ReturnToPool(6f);
    }
}
