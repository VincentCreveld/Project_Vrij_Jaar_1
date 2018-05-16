using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator34Controller : MonoBehaviour {
    public bool lightsBool;
    public GameObject[] Lifts;
    public GameObject[] Lights;
    public GameObject[] ElevatorButtons;
    public GameObject CameraPlane;


	// Use this for initialization
	void Start () {
        Lifts[0].SetActive(true);
        Lifts[1].SetActive(false);
        Lights[0].SetActive(true);
        Lights[1].SetActive(false);
        for (int i = 0; i < ElevatorButtons.Length; i++)
        {
            ElevatorButtons[i].SetActive(false);
        }
        StartCoroutine(LevelSequence());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator LevelSequence()
    {
        int i = 0;
        Debug.Log("Started Seq");
        yield return new WaitForSeconds(10f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(FlipBool(lightsBool));
        yield return new WaitForSeconds(0.888f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(FlipBool(lightsBool));
        yield return new WaitForSeconds(0.6f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(FlipBool(lightsBool));
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(FlipBool(lightsBool));
        yield return new WaitForSeconds(1f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(FlipBool(lightsBool));
        yield return new WaitForSeconds(3f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(FlipBool(lightsBool));
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(FlipBool(lightsBool));
        yield return new WaitForSeconds(0.8f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(false);
        CameraPlane.SetActive(true);
        CameraPlane.GetComponent<MeshRenderer>().material.color = new Vector4(0f, 0f, 0f, 1f);
        yield return new WaitForSeconds(5f);
        Debug.Log("Seq P" + i);
        i++;
        Lifts[0].SetActive(false);
        Lifts[1].SetActive(true);
        yield return new WaitForSeconds(0.888f);
        CameraPlane.SetActive(false);
        CameraPlane.GetComponent<MeshRenderer>().material.color = new Vector4(0f, 0f, 0f, 0f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[1].SetActive(true);
        yield return new WaitForSeconds(4f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[1].SetActive(false);
        yield return new WaitForSeconds(1f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[1].SetActive(true);
        yield return new WaitForSeconds(1f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[1].SetActive(false);
        CameraPlane.SetActive(true);
        CameraPlane.GetComponent<MeshRenderer>().material.color = new Vector4(0f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.888f);
        Debug.Log("Seq P" + i);
        i++;
        
        Lifts[0].SetActive(true);
        Lifts[1].SetActive(false);
        yield return new WaitForSeconds(3f);
        CameraPlane.SetActive(false);
        CameraPlane.GetComponent<MeshRenderer>().material.color = new Vector4(0f, 0f, 0f, 0f);
        Debug.Log("Seq P" + i);
        i++;
        Lights[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        Debug.Log("Finished Seq");
        ElevatorButtons[3].SetActive(true);
    }

    public bool FlipBool(bool b)
    {
        CameraPlane.SetActive(b);
        lightsBool = !b;
        return !b;
        
    }

}
