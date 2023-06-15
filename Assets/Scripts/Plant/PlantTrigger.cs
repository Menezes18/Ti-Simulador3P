using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Debug;

public class PlantTrigger : MonoBehaviour
{
   public Plant plantedPlant;
   public CicloDiaNoite cicloDiaNoite;
   public Estacao estacaoAtual;

   public TipoEstacao tipoEstacao;

   public Transform t;

   private LayerMask playerLayer;
   private int Atualdias;
   private int diasNaEstacao = 1;
   private int dias;

   private float segundos;
   private float multiplacador;
   private float soma = 86400f;

   private int idade = 0;
   private bool idadeciclo = false;
   private bool plantaInstanciada = false;
   private bool ciclo1 = true;
   private bool ciclo2 = false;
   private bool ciclo3 = false;
   public int diaciclo = 0;
   public GameObject droppedItem;
   public GameObject previousPrefab;
   public float dropDistance = 2f;
   public string tagBloco = "Player";
   public int distancialimit = 2;
   public bool acabou = false;

    private void Start()
    {
      playerLayer = LayerMask.GetMask("Player");
      Transform filho = transform.Find("SementeTrigo");
      t = transform;
      cicloDiaNoite = FindObjectOfType<CicloDiaNoite>();
      tipoEstacao = plantedPlant.TipoEstacao;
      dias = plantedPlant.dias;
      diasNaEstacao = cicloDiaNoite.diaTest;    
      multiplacador = 86400 / cicloDiaNoite.duracaoDoDia;
    }

   public void Update() 
   {

      estacaoAtual = cicloDiaNoite.estacaoAtual;
      tipoEstacao = plantedPlant.TipoEstacao;
      Atualdias = cicloDiaNoite.diaAtual;

      plantaEstagio();

      GameObject[] blocos = GameObject.FindGameObjectsWithTag(tagBloco);
        foreach (GameObject bloco in blocos)
        {
            float distancia = Vector3.Distance(transform.position, bloco.transform.position);

            if (distancia <= distancialimit)
            {
               if(acabou)
               {
                  if(Keyboard.current.eKey.wasPressedThisFrame)
                  {
                    UnityEngine.Debug.Log("aaaaa");
                    TryDropItem();

                  }
               }
            }
        }
      
   }


   private void FixedUpdate()
   {
      if(idadeciclo)
      {
      ciclodiaPlant();

      }
   }


public void plantaEstagio()
{
   if (tipoEstacao.ToString() == estacaoAtual.ToString())
   {
      
      idadeciclo = true;
      if (!plantaInstanciada)
      { 
         diaciclo = dias / 3;
         if (idade == diaciclo && ciclo1)
         {
            GetPrefab(1, t);
            ciclo1 = false;
            ciclo2 = true;
         }else if(idade == diaciclo * 2 && ciclo2)
         {
            GetPrefab(2, t);
            ciclo2 = false;
            ciclo3 = true;
         }else if(idade == dias && ciclo3)
         {

            GetPrefab(3, t);
            ciclo3 = false;
            idadeciclo = false;
            acabou = true;
            
         }

         
      }
   }
}

   public GameObject GetPrefab(int estagio, Transform parent)
   {

        if (estagio >= 1 && estagio <= 3)
        {
            if (previousPrefab != null)
            {
                Destroy(previousPrefab);
            }
            GameObject prefab = plantedPlant.prefabs[estagio - 1];
            GameObject newPrefab = Instantiate(prefab, parent);
            previousPrefab = newPrefab;
            return newPrefab;
        }
        else
        {
            //Debug.Log("Estágio inválido");
            return null;
        }
   }

 private void TryDropItem()
         {
            Collider[] playerColliders = Physics.OverlapSphere(transform.position, dropDistance, playerLayer);
            foreach (Collider playerCollider in playerColliders)
            {
                  if (playerCollider.CompareTag("Player"))  // Verifica se o objeto é o jogador
                  {
                     DropItem();
                     break;
                  }
            }
         }

    private void DropItem()
    {
          UnityEngine.Debug.Log("aaaaa");
        Instantiate(droppedItem, transform.position, Quaternion.identity);  // Instancia o item dropado
        Destroy(gameObject);  // Destroi o objeto da planta
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, dropDistance);
    }
public void ciclodiaPlant()
{
   segundos += Time.deltaTime * multiplacador;
       
        if (segundos >= soma)
        {
            segundos = 0;
            idade++;
        }
}


}
