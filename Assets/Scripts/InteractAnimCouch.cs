using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractAnimCouch : MonoBehaviour, IInteractable
{
    public string TriggerName;
    public bool Triggered;
    public bool GetAnimFromParent;
    Animator anim;

    public void Start()
    {
        if (!GetAnimFromParent)
        {
            anim = GetComponent<Animator>();
        }
        else
        {
            anim = transform.parent.GetComponent<Animator>();
        }
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
        anim.SetBool(TriggerName, true);
        Triggered = true;
        if (GetComponent<HighlightableObj>() != null)
        {
            GetComponent<HighlightableObj>().DeHighlight();
        }else if(GetComponent<HighlightableList>() != null)
        {
            GetComponent<HighlightableList>().DeHighlight();
        }
        GetComponent<Collider>().enabled = false;
    }

}

