using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPZone : MonoBehaviour
{

    public GameObject cylinder;
    public Material baseMat;
    public Material hoverMat;


    public void hover()
    {
        cylinder.GetComponent<Renderer>().material = hoverMat;
    }
    public void unHover()
    {
        cylinder.GetComponent<Renderer>().material = baseMat;
    }

}
