using UnityEngine;

public class Attacker : MonoBehaviour, IAttack
{
    public int Damage { get { return damage; } }
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    [SerializeField]
    private float attackRefreshSpeed = 1.5f;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float attackOffset = 1f;
    [SerializeField]
    private float attackRadius = 1f;

    private float attackTimer;
    private Collider[] attackResults;

    private void Awake()
    {
        var animationImpactWatcher = GetComponentInChildren<AnimationImpactWatcher>();
        if (animationImpactWatcher != null)
        {
            animationImpactWatcher.OnImpact += AnimationImpactWatcher_OnImpact;
        }

        attackResults = new Collider[10];
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
    }

    /// <summary>
    /// Called by animation event via AnimationImpactWatcher
    /// </summary>
    private void AnimationImpactWatcher_OnImpact()
    {
        Vector3 position = transform.position + transform.forward * attackOffset;
        int hitCount = Physics.OverlapSphereNonAlloc(position, attackRadius, attackResults);

        for (int i = 0; i < hitCount; i++)
        {
            var takeHit = attackResults[i].GetComponent<ITakeHit>();
            if (takeHit != null)
            {
                takeHit.TakeHit(this);
            }
        }
    }

    public void Attack(ITakeHit target)
    {
        attackTimer = 0;
        target.TakeHit(this);
    }
}
