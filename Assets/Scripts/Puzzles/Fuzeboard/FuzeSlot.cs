using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzeSlot : MonoBehaviour
{
    private bool fuzeConnected = false;

    [SerializeField]
    public int ID;

    public event Action OnConnected;

    private void OnCollisionEnter(Collision collision)
    {
        if (!fuzeConnected)
        {
            if (collision.gameObject.CompareTag("Fuze"))
            {
                if (ID == collision.gameObject.GetComponent<Fuze>().ID)
                {
                    fuzeConnected = true;
                    collision.collider.enabled = false;
                    gameObject.transform.GetChild(1).GetComponent<Collider>().enabled = false;
                    OnConnected?.Invoke();
                }
            }
        }
    }
}
