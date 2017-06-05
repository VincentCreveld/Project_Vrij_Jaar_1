using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour, IInteractable {
    public GameObject objToChange;
    public Material mat1;
    Material currentMat;

    public void Start()
    {
        currentMat = GetComponent<Renderer>().material;
    }

    public void CheckInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        Debug.Log("Pressed");
        ChangeColors();
    }

    public void ChangeColors()
    {
        objToChange.GetComponent<Renderer>().material = mat1;
    }
}
