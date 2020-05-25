using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : AbilityBase, IDamage
{
    public int Damage { get { return damage; } }

    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float attackRadius = 2f;
    [SerializeField]
    private float impactDelay = 0.25f;
    [SerializeField]
    private float knockbackForce = 10f;

    private Collider[] attackResults;
    private LayerMask layerMask;

    private void Awake()
    {
        attackResults = new Collider[10];
        string currentLayer = LayerMask.LayerToName(gameObject.layer);
        layerMask = ~LayerMask.GetMask(currentLayer);
    }

    private void Attack()
    {
        attackTimer = 0;
        StartCoroutine(DoAttack());
    }

    private IEnumerator DoAttack()
    {
        yield return new WaitForSeconds(impactDelay);

        Vector3 position = transform.position + transform.forward;
        int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, attackResults, layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();
            if (takeHit != null)
            {
                takeHit.TakeHit(this);
            }

            var hitRigidbody = attackResults[i].GetComponent<Rigidbody>();
            if (hitRigidbody != null)
            {
                Vector3 direction = Vector3.Normalize(hitRigidbody.transform.position - transform.position);
                hitRigidbody.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }
        }
    }

    protected override void OnUse()
    {
        Attack();
    }
}
