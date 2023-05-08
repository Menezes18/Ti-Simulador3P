using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseSelect : MonoBehaviour
{
    public float maxDistance = 100;
    OutlineObject outlineObject;
    private Vector2 mousePosition;



    // Update is called once per frame
    void Update()
    {
        //mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (outlineObject != null)
            {
                if (outlineObject.transform != hit.transform)
                {
                    outlineObject.Deselect();
                }
            }

            outlineObject = hit.transform.GetComponent<OutlineObject>();
            if (outlineObject != null)
            {
                outlineObject.Select();
            }
        }
        else
        {
            if (outlineObject != null)
            {
                outlineObject.Deselect();
            }
            outlineObject = null;
        }
    }
}