using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzeboard : MonoBehaviour
{
    [SerializeField]
    List<Material> IDColours;

    [SerializeField]
    List<GameObject> FuzeSlots;

    private int fuzesAdded = 0;


    void Start()
    {
        for (int i = 0; i < FuzeSlots.Count; i++) 
        {
            FuzeSlots[i].GetComponent<FuzeSlot>().OnConnected += FuzeAdded;

            MeshRenderer[] temp = FuzeSlots[i].transform.GetChild(0).GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer mesh in temp)
            {
                mesh.material = IDColours[i];
            }
        }
    }

    public void FuzeAdded()
    {
        fuzesAdded++;

        if (fuzesAdded >= 3)
        {
            gameObject.GetComponent<Puzzle>().OnPuzzleCompleted();
        }
    }

}