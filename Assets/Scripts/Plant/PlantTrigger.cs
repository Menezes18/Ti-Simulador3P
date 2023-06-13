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
      
   }

   private void FixedUpdate()
   {
      if(idadeciclo)
      {
      ciclodiaPlant();

      }
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
      
      idadeciclo = true;
      if (!plantaInstanciada)
      { 
         diaciclo = dias / 3;
         if (idade == diaciclo && ciclo1)
         {
            plantedPlant.GetPrefab(1, t);
            ciclo1 = false;
            ciclo2 = true;
         }else if(idade == diaciclo * 2 && ciclo2)
         {
            plantedPlant.GetPrefab(2, t);
            ciclo2 = false;
            ciclo3 = true;
         }else if(idade == dias && ciclo3)
         {
            plantedPlant.GetPrefab(3, t);
            ciclo3 = false;
            idadeciclo = false;
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
