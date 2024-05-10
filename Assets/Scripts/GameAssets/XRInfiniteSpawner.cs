using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInfiniteSpawner : MonoBehaviour
{
    [SerializeField]
    XRBaseInteractable m_InteractablePrefab;

    XRBaseInteractor m_Socket;

    private void Awake()
    {
        m_Socket = GetComponent<XRBaseInteractor>();
        Assert.IsNotNull(m_InteractablePrefab);
    }

    private void OnEnable()
    {
        m_Socket.selectExited.AddListener(OnSelectExited);
    }

    private void OnDisable()
    {
        m_Socket.selectExited.RemoveListener(OnSelectExited);
    }

    void OnSelectExited(SelectExitEventArgs args)
    {
        Transform socketTransform = m_Socket.transform;
        XRBaseInteractable interactable = Instantiate(m_InteractablePrefab, socketTransform.position, socketTransform.rotation);

        m_Socket.interactionManager.SelectEnter((IXRSelectInteractor)m_Socket, interactable);
    }
}
