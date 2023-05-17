using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
{
    Building otherBuilding = other.GetComponentInParent<Building>();
    if (otherBuilding != null)
    {
        Debug.Log("Um bloco colidiu com: " + otherBuilding.AssignedData.DisplayName);
    }
}
    
}
