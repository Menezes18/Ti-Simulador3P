using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Controll : MonoBehaviour
{
    public GameObject HUD_NPCQuest;
    public GameObject HUD_NPCQuestComplet;
    public static bool isQuestStart;
    bool isQuestComplet;
    public static int Planta;
    void Start()
    {
        HUD_NPCQuest.SetActive(false);
        HUD_NPCQuestComplet.SetActive(false);
        isQuestStart = false;
    }
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag == "Player")
        {
            HUD_NPCQuest.SetActive (true);
            isQuestStart = true;
        }

        if(Planta == 1)
        {
            isQuestComplet = true;
        }

        if (hit.gameObject.tag == "Player" && isQuestComplet)
        {
            HUD_NPCQuest.SetActive(false);
            HUD_NPCQuestComplet.SetActive(true);
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.tag == "Player")
        {
            HUD_NPCQuest.SetActive(false);
            HUD_NPCQuestComplet.SetActive(false);
        }
    }
}
