using UnityEngine;

public class Projectile : PooledMonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
