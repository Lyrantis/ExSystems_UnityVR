using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FuzeSlot : MonoBehaviour
{
    private bool fuzeConnected = false;

    [SerializeField]
    public int ID;

    public event Action OnConnected;

    public void CheckFuse()
    {
        Fuze fuze = GetComponentInChildren<XRExclusiveSocket>().selectTarget.gameObject.GetComponent<Fuze>();

        if (fuze.ID == ID)
        {
            fuze.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            OnConnected.Invoke();
            Destroy(this);
        }
    }
}
