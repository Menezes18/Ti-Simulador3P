using UnityEngine;

public class MultiTag : MonoBehaviour
{
    public string[] tags;
    public string componentName;

    void Start()
    {
        Component[] components = gameObject.GetComponentsInChildren(typeof(Component));

        foreach (Component component in components)
        {
            if (component.GetType().Name == componentName)
            {
                foreach (string tag in tags)
                {
                    component.gameObject.tag += "," + tag;
                }
            }
        }
    }
}
