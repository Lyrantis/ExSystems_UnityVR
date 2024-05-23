using System;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private List<Pipe> connections;

    [SerializeField]
    public bool isStart = false;

    [SerializeField]
    public bool isEnd = false;

    public event Action OnConnectionGained;

    private float lastKnobRotation = float.NegativeInfinity;
    // Start is called before the first frame update
    void Start()
    {
        connections = new List<Pipe>();

        gameObject.transform.Rotate(-90.0f, 0.0f, 0.0f);

        if (!isStart & !isEnd)
        {
            int degreesToRotate = UnityEngine.Random.Range(0, 4) * 90; 
            gameObject.transform.Rotate(0.0f, degreesToRotate, 0.0f);
        }
        else
        {
            gameObject.GetComponent<XRKnob>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            GameObject currentParent = collision.gameObject;
            while (currentParent.transform.parent != null)
            {
                currentParent = currentParent.transform.parent.gameObject;
            }
            if (connections != null)
            {
                connections.Add(currentParent.GetComponent<Pipe>());
            }

            OnConnectionGained?.Invoke();
        }
    }
    
    public bool CheckForStartConnection(List<Pipe> alreadyChecked)
    {
        if (connections != null)
        {
            foreach (Pipe pipe in connections)
            {
                if (!alreadyChecked.Contains(pipe))
                {
                    alreadyChecked.Add(pipe);
                    if (pipe.isStart)
                    {
                        return true;
                    }
                    else
                    {
                        if (pipe.CheckForStartConnection(alreadyChecked))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            connections.Remove(collision.gameObject.GetComponent<Pipe>());
        }
    }


    public void SetRotation()
    {
        if (lastKnobRotation != float.NegativeInfinity)
        {
            gameObject.transform.Rotate(new Vector3(0.0f, (gameObject.GetComponent<XRKnob>().value - lastKnobRotation) * 90, 0.0f));
            lastKnobRotation = gameObject.GetComponent<XRKnob>().value;
        }
        else
        {
            gameObject.transform.Rotate(new Vector3(0.0f, gameObject.GetComponent<XRKnob>().value * 360, 0.0f));
            lastKnobRotation = gameObject.GetComponent<XRKnob>().value;
        }
        
    }
}
