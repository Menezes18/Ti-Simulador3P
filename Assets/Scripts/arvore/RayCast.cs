using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayCast : MonoBehaviour
{
    private HotbarDisplay hotbarDisplay;
    public int rayLength = 1;

    public GameObject tree;
    public bool isMachadoUsed = false;

    public void Update()
    {

        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            if (hit.collider.gameObject.tag == "Tree")
            {
                tree = hit.collider.gameObject;
            

                    if (Keyboard.current.eKey.wasPressedThisFrame)
                    {
                        Debug.Log(tree.GetComponent<TreeControler>().treeHealth -= 1);
                        tree.GetComponent<TreeControler>().treeHealth -= 1;
                        
                        if (tree.GetComponent<TreeControler>().treeHealth <= 0)
                        {
                            Rigidbody treeRigidbody = tree.GetComponent<Rigidbody>();
                            if (treeRigidbody != null)
                            {
                                treeRigidbody.AddForce(Vector3.up * 1000f);
                            }
                        }
                    }
                
            }
        }
    }
}
