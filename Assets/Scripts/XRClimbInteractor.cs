using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRClimbInteractor : XRDirectInteractor
{
    public Action<string> ClimbActivated;
    public Action<string> ClimbDeactivated;

    private string _controllerInUse;

    protected override void Start()
    {
        base.Start();
        _controllerInUse = gameObject.name;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactableObject.transform.gameObject.CompareTag("Climbable"))
        {
            ClimbActivated?.Invoke(_controllerInUse);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        ClimbDeactivated?.Invoke(_controllerInUse);
    }
}
