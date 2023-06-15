using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaController : MonoBehaviour
{

    void Update()
    {

    }
    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.tag == "Player" && NPC_Controll.isQuestStart)
        {
            gameObject.SetActive(false);
            NPC_Controll.Planta = 1;
        }
    }
    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            gameObject.SetActive(true);
        }
    }
}
