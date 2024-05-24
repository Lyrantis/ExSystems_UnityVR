using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuze : MonoBehaviour
{
    [SerializeField]
    public int ID = 0;

    [SerializeField]
    public List<Material> IDcolours;

    private void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<MeshRenderer>().material = IDcolours[ID];
        }
    }
}
