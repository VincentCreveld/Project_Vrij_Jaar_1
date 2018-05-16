using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsPuzzlePiece : MonoBehaviour, IInteractable {

    public int id;
    public int originalId;
    public bool selected;
    DrugsPuzzleController drugsPuzContr;

	void Start ()
    {
        drugsPuzContr = transform.parent.parent.GetComponent<DrugsPuzzleController>();
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
        drugsPuzContr.CheckOrder(originalId);
    }

}
