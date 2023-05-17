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


   public int Atualdias;
   public int diasNaEstacao = 1;
   public int dias;


    private void Start()
    {
      cicloDiaNoite = FindObjectOfType<CicloDiaNoite>();
      tipoEstacao = plantedPlant.TipoEstacao;
      dias = plantedPlant.dias;
      diasNaEstacao = cicloDiaNoite.diaTest;    
    }

   public void Update() 
   {
      estacaoAtual = cicloDiaNoite.estacaoAtual;
      tipoEstacao = plantedPlant.TipoEstacao;
      Atualdias = cicloDiaNoite.diaAtual;

      plantaEstagio();
   }

private bool plantaInstanciada = false;
private bool ciclo1 = true;
private bool ciclo2 = false;
private bool ciclo3 = false;
public int diaciclo = 0;

public void plantaEstagio()
{
   if (tipoEstacao.ToString() == estacaoAtual.ToString())
   {
      cicloDiaNoite.pode2 = true;

      if (!plantaInstanciada)
      { 
         diaciclo = dias / 3;
         if (cicloDiaNoite.diaTest == diaciclo && ciclo1)
         {
            plantedPlant.GetPrefab(1, t);
            ciclo1 = false;
            ciclo2 = true;
         }else if(cicloDiaNoite.diaTest == diaciclo * 2 && ciclo2)
         {
            plantedPlant.GetPrefab(2, t);
            ciclo2 = false;
            ciclo3 = true;
         }else if(cicloDiaNoite.diaTest == dias && ciclo3)
         {
            plantedPlant.GetPrefab(3, t);
            ciclo3 = false;
         }

         
      }
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






   
}
