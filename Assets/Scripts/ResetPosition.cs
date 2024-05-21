using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    private Vector3 m_startPosition;
    private Quaternion m_startRotation;
    // Start is called before the first frame update
    void Start()
    {
        m_startPosition = gameObject.transform.position;
        m_startRotation = gameObject.transform.rotation;
    }

    public void ResetPositionToStart()
    {
        Debug.Log("Resetting");
        gameObject.transform.position = m_startPosition;
        gameObject.transform.rotation = m_startRotation;
    }
}