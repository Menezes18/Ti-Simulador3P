using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControler : MonoBehaviour
{

    public float pushForce = 50000f;
    private bool isTreeDown = false;

    public int treeHealth = 5;

    public Transform madeira;
    public Transform folha;
    public GameObject tree;

    public int speed = 8;

    void Start()
    {
        tree = this.gameObject;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    void Update()
    {
        if (treeHealth <= 0)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            DestroyTree();
        }
    }

    void DestroyTree()
    {
        StartCoroutine(DestroyAfterDelay(8));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(tree);

        Vector3 position = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f));
        Instantiate(madeira, tree.transform.position + new Vector3(0, 0, 0) + position, Quaternion.identity);
        Instantiate(madeira, tree.transform.position + new Vector3(2, 2, 0) + position, Quaternion.identity);
        Instantiate(madeira, tree.transform.position + new Vector3(5, 5, 0) + position, Quaternion.identity);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isTreeDown && collision.gameObject.tag == "Player")
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.AddForce(transform.forward * pushForce); // adicionar uma grande força ao jogador para afastá-lo da árvore
            }
        }
    }
}
