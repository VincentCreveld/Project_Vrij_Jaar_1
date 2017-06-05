using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour{

    public GameObject StartPoint;
    public GameObject ExitPoint;
    public GameObject CompletedLight;
    public bool completed;
    public bool inMaze;
    public GameObject Backboard;
    Material backBoardMat;

    void Start()
    {
        backBoardMat = Backboard.GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
    }


    public void StartMaze()
    {
        Debug.Log("MazeStarted");
        inMaze = true;
        Backboard.GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    public void ExitMaze()
    {
        inMaze = false;
        Backboard.GetComponent<MeshRenderer>().material.color = Color.black;
    }

    public void FinishMaze()
    {
        completed = true;
        inMaze = false;
        Backboard.GetComponent<MeshRenderer>().material.color = CompletedLight.GetComponent<MeshRenderer>().material.color = Color.green;
    }

}
