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

    private GameObject previousPrefab;
    public GameObject mortePrefab;
    public GameObject item;

    public GameObject GetPrefab(int estagio, Transform parent)
    {
        if (morte)
        {
            Morte(parent);
            return null;
        }

        if (estagio >= 1 && estagio <= 3)
        {
            if (previousPrefab != null)
            {
                Destroy(previousPrefab);
            }

            GameObject prefab = prefabs[estagio - 1];
            GameObject newPrefab = Instantiate(prefab, parent);
            previousPrefab = newPrefab;
            return newPrefab;
        }
        else
        {
            Debug.Log("Estágio inválido");
            return null;
        }
    }

    private void Morte(Transform parent)
    {
        if (previousPrefab != null)
        {
            Destroy(previousPrefab);
        }


        if (mortePrefab != null)
        {
            GameObject newPrefab = Instantiate(mortePrefab, parent);
            previousPrefab = newPrefab;
        }
        else
        {
            Debug.Log("Prefab de morte não encontrado.");
        }
    }
}
