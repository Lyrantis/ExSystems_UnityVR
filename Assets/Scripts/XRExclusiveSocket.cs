using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRExclusiveSocket : XRSocketInteractor
{
    [SerializeField]
    string _acceptedTag;

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        if (!base.CanHover(interactable))
        {
            return false;
        }

        if (interactable.transform.CompareTag(_acceptedTag))
        {
            return true;
        }

        return false;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        if (!base.CanSelect(interactable))
        {
            return false;
        }

        if (interactable.transform.CompareTag(_acceptedTag))
        {
            return true;
        }

        return false;
    }
}
