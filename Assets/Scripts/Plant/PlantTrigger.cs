using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTrigger : MonoBehaviour
{
   public Plant plantedPlant;
   public CicloDiaNoite cicloDiaNoite;
   public Estacao estacaoAtual;

   public TipoEstacao tipoEstacao;

   public Transform t;


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

   public string tagBloco = "Player";
   public int distancialimit = 2;
   public bool acabou = false;

    private void Start()
    {
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
               
            }
        }
      
   }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distancialimit);
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
            
         }

         
      }
   }
}
   public GameObject GetPrefab(int estagio, Transform parent)
   {

        if (estagio >= 1 && estagio <= 3)
        {
            if (plantedPlant.previousPrefab != null)
            {
                Destroy(plantedPlant.previousPrefab);
            }

            GameObject prefab = plantedPlant.prefabs[estagio - 1];
            GameObject newPrefab = Instantiate(prefab, parent);
            plantedPlant.previousPrefab = newPrefab;
            return newPrefab;
        }
        else
        {
            Debug.Log("Estágio inválido");
            return null;
        }
   }

public void DropItem()
{
    if (plantaInstanciada && !ciclo1 && !ciclo2 && !ciclo3)
    {
        Instantiate(plantedPlant.item, transform.position, Quaternion.identity);
        plantaInstanciada = false;
    }
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
