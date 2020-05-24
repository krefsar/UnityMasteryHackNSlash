using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private static Dictionary<PooledMonoBehaviour, Pool> pools = new Dictionary<PooledMonoBehaviour, Pool>();

    private Queue<PooledMonoBehaviour> objects = new Queue<PooledMonoBehaviour>();
    private PooledMonoBehaviour prefab;

    public static Pool GetPool(PooledMonoBehaviour prefabToGet)
    {
        if (pools.ContainsKey(prefabToGet))
        {
            return pools[prefabToGet];
        }

        Pool pool = new GameObject("Pool-" + prefabToGet.name).AddComponent<Pool>();
        pool.prefab = prefabToGet;

        pools.Add(prefabToGet, pool);

        return pool;
    }

    public T Get<T>() where T : PooledMonoBehaviour
    {
        if (objects.Count == 0)
        {
            GrowPool();
        }

        PooledMonoBehaviour pooledObject = objects.Dequeue();
        return pooledObject as T;
    }

    private void GrowPool()
    {
        for (int i = 0; i < prefab.InitialPoolSize; i++)
        {
            PooledMonoBehaviour pooledObject = Instantiate(prefab) as PooledMonoBehaviour;
            pooledObject.gameObject.name += " " + i;
            pooledObject.transform.SetParent(transform);

            pooledObject.OnReturnToPool += AddObjectToAvailableQueue;

            pooledObject.gameObject.SetActive(false);
        }
    }

    private void AddObjectToAvailableQueue(PooledMonoBehaviour pooledObject)
    {
        pooledObject.transform.SetParent(transform);
        objects.Enqueue(pooledObject);
    }
}