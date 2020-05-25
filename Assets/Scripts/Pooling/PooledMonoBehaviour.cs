using System;
using System.Collections;
using UnityEngine;

public class PooledMonoBehaviour : MonoBehaviour
{
    public int InitialPoolSize { get { return initialPoolSize; } }
    public event Action<PooledMonoBehaviour> OnReturnToPool;

    [SerializeField]
    private int initialPoolSize = 50;

    public T Get<T>(bool enable = true) where T : PooledMonoBehaviour
    {
        Pool pool = Pool.GetPool(this);
        T pooledObject = pool.Get<T>();

        if (enable)
        {
            pooledObject.gameObject.SetActive(true);
        }

        return pooledObject;
    }

    public T Get<T>(Vector3 position, Quaternion rotation) where T : PooledMonoBehaviour
    {
        T pooledObject = Get<T>();
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;

        return pooledObject;
    }

    private void OnDisable()
    {
        if (OnReturnToPool != null)
        {
            OnReturnToPool(this);
        }
    }

    protected void ReturnToPool(float delay = 0f)
    {
        StartCoroutine(ReturnToPoolAfterSeconds(delay));
    }

    private IEnumerator ReturnToPoolAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
