using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Interactable : MonoBehaviour, IInteractable {

    public bool Triggered;
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void CheckInput()
    {
        if (Input.GetButtonDown("Fire1") && !Triggered)
        {
            Interact();
        }
    }

    public void Interact()
    {
        Debug.Log("Pressed");
        anim.SetBool("Trigger", true);
        Triggered = true;
        GetComponent<HighlightableObj>().DeHighlight();
        GetComponent<Collider>().enabled = false;
    }

}
