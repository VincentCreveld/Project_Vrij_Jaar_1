using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugsPuzzleController : MonoBehaviour {

    public int progress = 0;
    public GameObject SolveTrigger;
    public GameObject flaskParent;
    public Transform solvePosParent;
    public GameObject[] flaskPositions;
    public Vector3[] originalPositions;
    Vector3[] positions;

    public Level2Controller level2control;

    public bool NeedsOrder;

    public bool Solved;
    public int[] solution;

    void Start()
    {
        //level2control = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelControllerlv2>();
        flaskPositions = new GameObject[flaskParent.transform.childCount];
        positions = new Vector3[solvePosParent.transform.childCount];
        originalPositions = new Vector3[flaskParent.transform.childCount];
        for (int i = 0; i < flaskParent.transform.childCount; i++)
        {
            flaskPositions[i] = flaskParent.transform.GetChild(i).gameObject;
            flaskPositions[i].GetComponent<DrugsPuzzlePiece>().id = i;
            flaskPositions[i].GetComponent<DrugsPuzzlePiece>().originalId = i;
            flaskPositions[i].name = "Flask " + i;
            
        }
        for (int i = 0; i < flaskPositions.Length; i++)
        {
            originalPositions[i] = flaskPositions[i].transform.position;
        }
        UpdatePositions();
    }

    void Update()
    {
        CheckSolve();
    }

    void UpdatePlacements()
    {
        for (int i = 0; i < flaskParent.transform.childCount; i++)
        {
            flaskPositions[i].transform.position = positions[i];
            flaskPositions[i].GetComponent<DrugsPuzzlePiece>().id = i;
            
        }
    }

    void UpdatePositions()
    {
        for (int i = 0; i < solvePosParent.childCount; i++)
        {
            positions[i] = solvePosParent.GetChild(i).position;
        }
    }

    public void CheckOrder(int _int)
    {
        if (NeedsOrder)
        {
            if (_int == solution[progress])
            {
                flaskPositions[solution[progress]].transform.position = positions[solution[progress]];
                flaskPositions[_int].GetComponent<Collider>().enabled = false;
                progress++;
            }
            else
            {
                ResetPuzzle();
            }
        }
        else
        {
            flaskPositions[_int].transform.position = positions[_int];
            flaskPositions[_int].GetComponent<Collider>().enabled = false;
            progress++;
        }
    }

    private void ResetPuzzle()
    {
        for (int i = 0; i < flaskParent.transform.childCount; i++)
        {
            Debug.Log("Resetting Pos");
            flaskPositions[i].transform.position = originalPositions[i];
            flaskPositions[i].GetComponent<Collider>().enabled = true;
            progress = 0;
            
            //flaskPositions[i].GetComponent<ShufflePiece>().id = i;
        }
        level2control.ResetDrugsPuzzleAudio();
    }

    void CheckSolve()
    {
        bool solve = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            if (progress != solution.Length)
            {
                solve = false;
            }
        }

        if (solve)
        {
            Solved = true;
            
        }

    }

    public IEnumerator FinishLvl()
    {

        yield return new WaitForSeconds(2);

        yield return new WaitForSeconds(3);
        GameManager.SetLevelAsComplete(2);
    }

}
