using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    public bool CanAttack { get { return attackTimer >= attackRefreshSpeed; } }

    protected float attackTimer;

    [SerializeField]
    private float attackRefreshSpeed = 1.5f;
    [SerializeField]
    private PlayerButton button;

    private Controller controller;

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (controller != null &&
            CanAttack &&
            controller.ButtonDown(button))
        {
            OnTryUse();
        }
    }

    public void SetController(Controller controller)
    {
        this.controller = controller;
    }

    protected abstract void OnTryUse();
}