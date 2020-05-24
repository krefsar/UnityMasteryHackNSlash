using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Character : MonoBehaviour, ITakeHit
{
    public static List<Character> AllCharacters = new List<Character>();

    public int Damage { get { return damage;  } }

    private Animator animator;
    private Controller controller;
    private Attacker attacker;

    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int maxHealth = 10;

    private int currentHealth;

    private void Awake()
    {
        attacker = GetComponent<Attacker>();
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;

        if (AllCharacters.Contains(this) == false)
        {
            AllCharacters.Add(this);
        }
    }

    private void Update()
    {
        Vector3 direction = controller.GetDirection();
        if (direction.magnitude > 0.4f)
        {
            transform.position += direction * Time.deltaTime * moveSpeed;
            transform.forward = direction * 360f;

            animator.SetFloat("Speed", direction.magnitude);
        } else
        {
            animator.SetFloat("Speed", 0f);
        }

        if (controller.attackPressed)
        {
            if (attacker.CanAttack)
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    private void OnDisable()
    {
        if (AllCharacters.Contains(this))
        {
            AllCharacters.Remove(this);
        }
    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }

    public void TakeHit(IAttack attacker)
    {
        currentHealth -= attacker.Damage;
    }
}
