using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePadlock : MonoBehaviour
{
    public int ID = 0;

    [SerializeField]
    GameObject objectToLock;

    private void Start()
    {
        if (objectToLock != null)
        {
            objectToLock.GetComponent<Collider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            if (other.GetComponent<SimpleKey>().ID == ID)
            {
                Unlock(other.gameObject);
            }
        }
    }

    public void Unlock(GameObject keyUsed)
    {
        if (keyUsed != null)
        {
            Destroy(keyUsed);
        }

        if (objectToLock != null)
        {
            objectToLock.GetComponent<Collider>().enabled = true;
        }
        Destroy(this.gameObject);
        Destroy(this);
    }
}
