using UnityEngine;

public class ObjectInstantiator : MonoBehaviour
{
    public GameObject objetoInstanciado;

    public void InstantiateObject(GameObject prefab)
    {
        // Instanciar o objeto
        GameObject novoObjeto = Instantiate(prefab);

        // Armazenar uma referência ao objeto instanciado
        objetoInstanciado = novoObjeto;
    }
}
