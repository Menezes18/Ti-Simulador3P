using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoEstacao
{
    Nenhum,
    Outono, 
    Inverno, 
    Primavera,
    Verao
}
public enum Prefabs
{
    PrefabEstagio1,
    PrefabEstagio2,
    PrefabEstagio3


}
[CreateAssetMenu(menuName = "Plant System/Semente")]
public class Plant : ScriptableObject
{
    public string Name;
    public TipoEstacao TipoEstacao;
    public float quantidadeAgua;
    public bool morte;
    public List<GameObject> prefabs;
    public float tempoPlant;

    public GameObject GetPrefab(int estagio)
    {
        if (estagio >= 1 && estagio <= 3)
        {
            return prefabs[estagio - 1];
        }
        else
        {
            Debug.Log("teste");
            return null;
        }

    }
}
