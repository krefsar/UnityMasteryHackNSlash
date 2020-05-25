using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : PooledMonoBehaviour, ITakeHit, IDie
{
    public static List<Character> AllCharacters = new List<Character>();

    public int Damage { get { return damage;  } }
    public bool IsDead { get { return currentHealth <= 0; } }
    public bool Alive { get; private set; }

    public event Action<int, int> OnHealthChanged = delegate { };

    public event Action<IDie> OnDied = delegate { };

    public event Action OnHit = delegate { };

    private Animator animator;
    private Controller controller;
    private IAttack attacker;
    private Rigidbody rb;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int maxHealth = 10;
    [SerializeField]
    private float controllerDeadZoneThreshold = 0.4f;

    private int currentHealth;

    private void Awake()
    {
        attacker = GetComponent<IAttack>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
        Alive = true;

        if (AllCharacters.Contains(this) == false)
        {
            AllCharacters.Add(this);
        }
    }

    private void Update()
    {
        Vector3 direction = controller.GetDirection();
        if (direction.magnitude > controllerDeadZoneThreshold)
        {
            Vector3 velocity = (direction * moveSpeed).With(y: rb.velocity.y);
            rb.velocity = velocity;
            transform.forward = direction * 360f;

            animator.SetFloat("Speed", direction.magnitude);
        } else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    protected override void OnDisable()
    {
        if (AllCharacters.Contains(this))
        {
            AllCharacters.Remove(this);
        }

        base.OnDisable();
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;

        foreach (var ability in GetComponents<AbilityBase>())
        {
            ability.SetController(controller);
        }
    }

    public void TakeHit(IDamage attacker)
    {
        if (IsDead)
        {
            return;
        }

        ModifyHealth(-attacker.Damage);
        OnHit();

        if (IsDead)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        ModifyHealth(healAmount);
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged(currentHealth, maxHealth);
    }

    private void Die()
    {
        Alive = false;
        OnDied(this);
    }
}