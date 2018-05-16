using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementElement : MonoBehaviour, IMoveElement {

    public GameObject Campos;
    public string pos;

    public GameObject ReturnCamPos()
    {
        Debug.Log("returning GO");
        return Campos;
    }

    public string ReturnString()
    {
        return pos;
    }

}
