using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObject : MonoBehaviour
{
    public float outlineSpeed = 0.01f;
    Outline outline;
    bool selecting;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void Select()
    {
        if (selecting)
            return;

        selecting = true;
        StopAllCoroutines();
        StartCoroutine(Selecting());
    }

    public void Deselect()
    {
        selecting = false;
        StopAllCoroutines();
        StartCoroutine(Deselecting());
    }

    IEnumerator Selecting()
    {
        while (outline.OutlineWidth < 5)
        {
            outline.OutlineWidth += outlineSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator Deselecting()
    {
        while (outline.OutlineWidth > 0)
        {
            outline.OutlineWidth -= outlineSpeed * Time.deltaTime;
            yield return null;
        }
    }
}