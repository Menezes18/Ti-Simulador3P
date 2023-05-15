using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    private void Start()
    {
        Collider[] allColliders = FindObjectsOfType<Collider>();

        foreach (Collider collider in allColliders)
        {
            if (collider is MeshCollider && collider != GetComponent<Collider>())
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collider);
            }
        }
    }

    
}
