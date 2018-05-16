using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShufflePuzzle : MonoBehaviour {

    public GameObject SolveTrigger;
    public GameObject[] children;
    Vector3[] positions;
    public GameObject arrayId1;
    public GameObject arrayId2;
    public bool Solved;
    public int[] solution;
    public bool RandomSolution;

    void Start ()
    {
        children = new GameObject[transform.childCount];
        positions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i).gameObject;
            children[i].GetComponent<ShufflePiece>().id = i;
            children[i].GetComponent<ShufflePiece>().originalId = i;
            children[i].name = "Piece " + i;
        }

        
        
        if (RandomSolution)
        {
            solution = new int[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                solution[i] = i;
            }
            RandomizeArray(solution);
        }
        UpdatePositions();
        RandomizeArray(children);
        UpdatePlacements();

    }
	
	void Update () {
		if(arrayId1 != null && arrayId2 != null && !Solved)
        {
            ShuffleArraypositions(arrayId1, arrayId2);
        }
        UpdatePlacements();
    }

    void ShuffleArraypositions(GameObject id1, GameObject id2)
    {
        GameObject tempObj = id1;
        children[id2.GetComponent<ShufflePiece>().id].GetComponent<ShufflePiece>().selected = false;
        children[id1.GetComponent<ShufflePiece>().id].GetComponent<ShufflePiece>().selected = false;
        children[id2.GetComponent<ShufflePiece>().id].GetComponent<IHighlightable>().DeHighlight();
        children[id1.GetComponent<ShufflePiece>().id].GetComponent<IHighlightable>().DeHighlight();
        children[id1.GetComponent<ShufflePiece>().id] = id2;
        children[id2.GetComponent<ShufflePiece>().id] = tempObj;
        UpdatePlacements();
        UpdatePositions();
        CheckSolve();
        arrayId1 = arrayId2 = tempObj = null;
    }

    void UpdatePlacements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i].transform.position = positions[i];
            children[i].GetComponent<ShufflePiece>().id = i;
        }
    }

    void UpdatePositions()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            positions[i] = children[i].transform.position;
        }
    }

    void CheckSolve()
    {
        bool solve = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            if(solution[i] != children[i].GetComponent<ShufflePiece>().originalId)
            {
                solve = false;
            }
        }

        if (solve)
        {
            Solved = true;
            SolveTrigger.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        //if (solution[0] == children[0].GetComponent<ShufflePiece>().originalId && solution[1] == children[1].GetComponent<ShufflePiece>().originalId && solution[2] == children[2].GetComponent<ShufflePiece>().originalId && solution[3] == children[3].GetComponent<ShufflePiece>().originalId)
    }

    static void RandomizeArray<T>(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }
}
