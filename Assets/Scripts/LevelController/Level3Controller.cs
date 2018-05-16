using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : MonoBehaviour {

    public AudioClip Solve;
    int failCount;
    AudioSource audiosource;

    public enum Level3State { start, LevelComplete }
    static Level3State level3State = Level3State.start;
    public Animator lift;

    bool routineStarted = false;
    public bool Solved = false;

    //Part 1 variables
    public GameObject[] PuzzleElements;
    public GameObject Parents;
    public bool SolvedP1;
    bool ChangedBoolArray = false;


    // Use this for initialization
    void Start () {
        audiosource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        if (GameManager.staticLevelsCleared[3] == true)
        {
            level3State = Level3State.LevelComplete;
        }
    }

    void Update()
    {
        switch (level3State)
        {
            case Level3State.start:
                CheckSolveP1();
                break;
            case Level3State.LevelComplete:
                if (routineStarted == false)
                {
                    StartCoroutine(LevelCleared());
                }
                break;
        }
    }

    public void CheckSolveP1()
    {
        bool solve = true;

        for (int i = 0; i < PuzzleElements.Length; i++)
        {
            if (PuzzleElements[i].GetComponent<Interactable>() != null && PuzzleElements[i].GetComponent<Interactable>().Triggered == false)
            {
                solve = false;
            }
            else if (PuzzleElements[i].GetComponent<InteractAnimCouch>() != null && PuzzleElements[i].GetComponent<InteractAnimCouch>().Triggered == false)
            {
                solve = false;
            }
        }

        if (solve)
        {
            audiosource.clip = Solve;
            audiosource.Play();
            level3State = Level3State.LevelComplete;
            if (!ChangedBoolArray)
            {
                Debug.Log("ReachedCode");
                lift.SetBool("DoorStateOpen", true);
                GameManager.SetLevelAsComplete(3);
                GameManager.canPlayClip3Elevator = true;
                Debug.Log(GameManager.staticLevelsCleared);
                ChangedBoolArray = true;

            }
        }
    }

    public IEnumerator LevelCleared()
    {
        routineStarted = true;
        for (int i = 0; i < PuzzleElements.Length; i++)
        {
            if (PuzzleElements[i].GetComponent<IInteractable>() != null)
            {
                PuzzleElements[i].GetComponent<IInteractable>().Interact();
            }
            if (PuzzleElements[i].GetComponent<InteractAnimCouch>() != null)
            {
                PuzzleElements[i].GetComponent<InteractAnimCouch>().Interact();
            }
        }
        yield return new WaitForSeconds(7f);
        Destroy(Parents);
        
        if (!ChangedBoolArray)
        {
            Debug.Log("ReachedCode");
            lift.SetBool("DoorStateOpen", true);
            GameManager.SetLevelAsComplete(3);
            GameManager.canPlayClip3Elevator = true;
            Debug.Log(GameManager.staticLevelsCleared);
            ChangedBoolArray = true;
            
        }
        
    }
}
