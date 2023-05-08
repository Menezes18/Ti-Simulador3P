using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
   public RayCast raycastScript;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Usaritem(string name)
    {
        Debug.Log("Usaritem " + name);
    }
}
