using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourBrick : MonoBehaviour
{
    public void SetColours(List<Material> inColours) 
    {
        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = inColours[i];
        }
    }

}
