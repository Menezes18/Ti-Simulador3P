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
   private BuildTools _building;
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
      droppedItem = plantedPlant.item;
      playerLayer = LayerMask.GetMask("Player");
      t = transform;
      
      _building = FindObjectOfType<BuildTools>();
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
            //GameObject childObject = transform.GetChild(0).gameObject;  // Obtém o primeiro filho do objeto atual
            //Renderer renderer = childObject.GetComponent<Renderer>();

            //Destroy(childObject);
            t.position = new Vector3(t.position.x, plantedPlant.transform, t.position.z);
            GetPrefab(1, t);
            ciclo1 = false;
            ciclo2 = true;
         }else if(idade == diaciclo * 2 && ciclo2)
         {
            t.position = new Vector3(t.position.x, plantedPlant.transform, t.position.z);
            GetPrefab(2, t);
            ciclo2 = false;
            ciclo3 = true;
         }else if(idade == dias && ciclo3)
         {
            t.position = new Vector3(t.position.x, plantedPlant.transform, t.position.z);
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

      GameObject droppedItemObject = Instantiate(droppedItem, transform.position, Quaternion.identity);  // Instancia o item dropado
      Rigidbody droppedItemRigidbody = droppedItemObject.GetComponent<Rigidbody>();  // Obtém o Rigidbody do item dropado

      // Adiciona uma força ao objeto recém-instanciado
      if (droppedItemRigidbody != null)
      {
         Vector3 forceDirection = new Vector3(0f, 0.3f, 0f);  // Defina a direção da força aqui
         float forceMagnitude = 10f;  // Defina a magnitude da força aqui
         droppedItemRigidbody.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
      }

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
