using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableChildrenOnEnter : MonoBehaviour
{
    [SerializeField]
    private float radius = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    private void OnValidate()
    {
        var collider = GetComponent<Collider>();
        if (collider == null)
        {
            collider = gameObject.AddComponent<SphereCollider>();
            ((SphereCollider)collider).radius = radius;
        }

        collider.isTrigger = true;
    }
}
