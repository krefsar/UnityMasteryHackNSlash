using UnityEngine;

public class Jump : AbilityBase
{
    private Rigidbody rb;

    [SerializeField]
    private float jumpForce = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnUse()
    {
        attackTimer = 0;
        rb.AddForce(Vector3.up * jumpForce);
    }
}
