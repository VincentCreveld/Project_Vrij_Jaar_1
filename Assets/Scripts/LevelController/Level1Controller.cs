using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour {
    
    public enum LevelState {cleanup, findBooze, LevelComplete }
    static LevelState levelState = LevelState.cleanup;
    GameObject cam;
    AudioSource audiosource;
    public GameObject lift;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;

    //Part 1 variables
    public GameObject[] PuzzleElements;
    public GameObject Parents;
    public bool SolvedP1;
    bool ChangedBoolArray = false;

    //Part 2 variables
    public GameObject[] MovementElements;
    public enum Position { elevator, cabinet, outside }
    public static Position position = Position.elevator;
    public Position curPosition = Position.elevator;
    public GameObject pillBox;
    public bool PillboxPickedUp = false;
    public bool PillsInBottle = false;
    public GameObject ParentsProgress;
    public GameObject ParentsDone;
    public GameObject ParentsP2;
    public bool SolvedP2;

    private void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        audiosource = cam.gameObject.GetComponent<AudioSource>();
        pillBox.GetComponent<Collider>().enabled = false;
        
        ChangedBoolArray = false;
        for (int i = 0; i < MovementElements.Length; i++)
        {
            MovementElements[i].SetActive(false);
        }
        if(GameManager.staticLevelsCleared[1] == true)
        {
            levelState = LevelState.LevelComplete;
        }
    }

    void Update ()
    {
        switch (levelState)
        {
            case LevelState.cleanup:
                if (!SolvedP1)
                {
                    CheckSolveP1();
                }
                break;
            case LevelState.findBooze:
                State2();
                break;
            case LevelState.LevelComplete:
                LevelCleared();
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
            else if(PuzzleElements[i].GetComponent<InteractAnimCouch>() != null && PuzzleElements[i].GetComponent<InteractAnimCouch>().Triggered == false)
            {
                solve = false;
            }
        }

        if (solve)
        {
            audiosource.clip = clip1;
            audiosource.Play();
            StartCoroutine(ClearLv1());
        }
    }

    public void ResetPosHighlights(Position pos)
    {
        position = pos;
        switch (pos)
        {
            case Position.elevator:
                MovementElements[0].SetActive(true);
                MovementElements[1].SetActive(true);
                MovementElements[2].SetActive(false);
                MovementElements[3].SetActive(false);
                break;
            case Position.cabinet:
                MovementElements[0].SetActive(true);
                MovementElements[1].SetActive(false);
                MovementElements[2].SetActive(true);
                MovementElements[3].SetActive(false);
                break;
            case Position.outside:
                MovementElements[0].SetActive(false);
                MovementElements[1].SetActive(false);
                MovementElements[2].SetActive(false);
                MovementElements[3].SetActive(true);
                break;
        }
    }

    public IEnumerator ClearLv1()
    {
        Debug.Log("cleared lv1");
        Destroy(Parents);
        SolvedP1 = true;

        yield return new WaitForSeconds(10f);

        StartCoroutine(cam.GetComponent<PlayerController>().MoveCam(MovementElements[0].GetComponent<IMoveElement>().ReturnCamPos()));

        yield return new WaitForSeconds(2f);

        levelState = LevelState.findBooze;
        position = Position.outside;
        pillBox.GetComponent<Collider>().enabled = true;
        ParentsP2.SetActive(true);
        ParentsProgress.SetActive(true);
        ParentsP2.GetComponent<Collider>().enabled = false;
        audiosource.clip = clip2;
        audiosource.Play();

    }

    public void State2()
    {
        ResetPosHighlights(position);
        if(pillBox == null)
        {
            PillboxPickedUp = true;
            ParentsP2.GetComponent<Collider>().enabled = true;
        }
        if(PillsInBottle == true)
        {
            
            ClearLv2();
        }
    }
    public void ClearLv2()
    {
        levelState = LevelState.LevelComplete;
        SolvedP2 = true;
        ParentsProgress.SetActive(false);
        ParentsDone.SetActive(true);
        lift.GetComponent<Animator>().SetBool("DoorStateOpen", true);
    }

    public void LevelCleared()
    {
        if(SolvedP1 == false)
        {
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
            Destroy(pillBox);
            Destroy(Parents);
            SolvedP1 = true;
        }
        if (SolvedP2 == false)
        {
            ClearLv2();
        }
        SolvedP2 = true;
        if (!ChangedBoolArray)
        {
            Debug.Log("reached code");
            audiosource.clip = clip3;
            audiosource.Play();
            GameManager.canPlayClip2Elevator = true;
            GameManager.SetLevelAsComplete(1);
            Debug.Log(GameManager.staticLevelsCleared);
            ChangedBoolArray = true;
        }
    }

}
