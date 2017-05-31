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
        StartCoroutine(ChangeColors());
    }

    public IEnumerator ChangeColors()
    {
        objToChange.GetComponent<Renderer>().material = mat1;
        yield return new WaitForSeconds(1.5f);
        objToChange.GetComponent<Renderer>().material = currentMat;
    }
}
