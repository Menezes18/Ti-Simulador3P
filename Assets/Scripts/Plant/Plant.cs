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
    PrefabEstagio3,
    PrefabMorte
}

[CreateAssetMenu(menuName = "Plant System/Semente")]
public class Plant : ScriptableObject
{
    public string Name;
    public TipoEstacao TipoEstacao;
    public float quantidadeAgua;
    public bool morte;
    public List<GameObject> prefabs;
    public int dias;
    public GameObject mortePrefab;
    public GameObject item;
    [Tooltip("Transform em Y")]
    public float transform; // mudar a transform em y das plantas quando vao ser instanciadas 


    
}
