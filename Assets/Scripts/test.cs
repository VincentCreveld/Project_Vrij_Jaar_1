using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Material mat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Select () {
        if (Input.GetMouseButtonDown(0))
        {
            Color tempColor = new Color(0.4f, 0, 0, 1);
            mat.SetColor("_EmissionColor", tempColor);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Color tempColor = new Color(0.0f, 0, 0, 1);
            mat.SetColor("_EmissionColor", tempColor);
        }
    }
}
