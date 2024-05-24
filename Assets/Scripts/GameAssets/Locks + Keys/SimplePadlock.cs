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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            if (collision.gameObject.GetComponent<SimpleKey>().ID == ID)
            {
                Unlock(collision.gameObject);
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
