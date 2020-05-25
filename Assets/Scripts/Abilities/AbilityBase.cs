using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    protected float attackTimer;

    private Animator animator;

    [SerializeField]
    private float attackRefreshSpeed = 1.5f;
    [SerializeField]
    private PlayerButton button;
    [SerializeField]
    protected string animationTrigger;

    private Controller controller;

    protected abstract void OnUse();

    private void Update()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        attackTimer += Time.deltaTime;

        if (ShouldTryUse())
        {
            if (!string.IsNullOrEmpty(animationTrigger))
            {
                animator.SetTrigger(animationTrigger);
            }

            OnUse();
        }
    }

    private bool ShouldTryUse()
    {
        return controller != null && CanAttack && controller.ButtonDown(button);
    }

    public void SetController(Controller controller)
    {
        this.controller = controller;
    }
}