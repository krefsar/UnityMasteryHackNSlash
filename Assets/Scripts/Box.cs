using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    private Rigidbody rb;
    [SerializeField]
    private float forceAmount = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeHit(IAttack attacker)
    {
        Vector3 direction = Vector3.Normalize(transform.position - attacker.transform.position);
        rb.AddForce(direction * forceAmount, ForceMode.Impulse);
    }
}
