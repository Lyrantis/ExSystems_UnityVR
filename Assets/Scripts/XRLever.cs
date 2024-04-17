using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class XRLever : XRBaseInteractable
{
    [SerializeField]
    Transform Handle = null;

    bool LeverInOnPosition;

    [SerializeField]
    bool SnapToExtents = false;

    [SerializeField]
    [Range(-90.0f, 90.0f)]
    float MaxAngle = 90.0f;

    [SerializeField]
    [Range(-90.0f, 90.0f)]
    float MinAngle = 90.0f;

    public UnityEvent OnLeverActivate = new UnityEvent();
    public UnityEvent OnLeverDeactivate = new UnityEvent();

    IXRSelectInteractor Interactor;

    protected override void OnEnable()
    {
        UnityEngine.Assertions.Assert.IsNotNull(Handle);
        base.OnEnable();
        selectEntered.AddListener(StartGrab);
        selectExited.AddListener(EndGrab);
    }

    protected override void OnDisable()
    {
        selectEntered.RemoveListener(StartGrab);
        selectExited.RemoveListener(EndGrab);
        base.OnDisable();
    }

    void StartGrab(SelectEnterEventArgs args)
    {
        Interactor = args.interactorObject;
    }
    
    void EndGrab(SelectExitEventArgs args)
    {
        if (!isSelected && SnapToExtents)
        {
            SetHandleAngle(LeverInOnPosition ? MaxAngle : MinAngle);
        }
        Interactor = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic && isSelected)
        {
            UpdateValue();
        }
    }

    Vector3 GetVectorToInteractor()
    {
        Vector3 direction = Interactor.GetAttachTransform(this).position - Handle.position;
        direction = transform.InverseTransformDirection(direction);
        direction.x = 0;

        return direction.normalized;
    }

    void UpdateValue()
    {
        Vector3 lookDirection = GetVectorToInteractor();
        float lookAngle = Mathf.Atan2(lookDirection.z, lookDirection.y) * Mathf.Rad2Deg;

        if (MinAngle < MaxAngle)
            lookAngle = Mathf.Clamp(lookAngle, MinAngle, MaxAngle);
        else
            lookAngle = Mathf.Clamp(lookAngle, MaxAngle, MinAngle);

        float maxAngleDistance = Mathf.Abs(MaxAngle - lookAngle);
        float minAngleDistance = Mathf.Abs(MinAngle - lookAngle);

        bool newValue = (maxAngleDistance < minAngleDistance);

        SetHandleAngle(lookAngle);

        SetValue(newValue);
    }

    void SetValue(bool isOn)
    {
        if (LeverInOnPosition == isOn)
        {
            return;
        }

        LeverInOnPosition = isOn;

        if (LeverInOnPosition)
        {
            OnLeverActivate.Invoke();
        }
        else
        {
            OnLeverDeactivate.Invoke();
        }
    }

    void SetHandleAngle(float angle)
    {
        Handle.localRotation = Quaternion.Euler(angle, 0.0f, 0.0f);
    }
}