using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportCasa : MonoBehaviour
{ public float teleportDistance = 3f;
    public Transform enterHouseTransform;
    public Transform exitHouseTransform;

    private void Update()
    {
        if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            TeleportToEnterHouse();
        }
        else if (Keyboard.current[Key.E].wasPressedThisFrame)
        {
            TeleportToExitHouse();
        }
    }

    private void TeleportToEnterHouse()
    {
        GameObject[] enterHouseObjects = GameObject.FindGameObjectsWithTag("CasaEntrar");
        if (enterHouseObjects.Length > 0)
        {
            Vector3 teleportPosition = enterHouseObjects[0].transform.position;
            if (Vector3.Distance(transform.position, teleportPosition) <= teleportDistance)
            {
                transform.position = teleportPosition;
            }
        }
    }

    private void TeleportToExitHouse()
    {
        GameObject[] exitHouseObjects = GameObject.FindGameObjectsWithTag("SairCasa");
        if (exitHouseObjects.Length > 0)
        {
            Vector3 teleportPosition = exitHouseObjects[0].transform.position;
            if (Vector3.Distance(transform.position, teleportPosition) <= teleportDistance)
            {
                transform.position = teleportPosition;
            }
        }
    }
}