using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : MonoBehaviour {

    float t;
    public float dur;

    public AudioClip foundBottles;
    public AudioClip Fail1;
    public AudioClip Fail2;
    public AudioClip Solve;
    int failCount;
    AudioSource audiosource;

    public GameObject lift;

    public enum Level2State { drugsp1, drugsp2, LevelComplete }
    static Level2State level2State = Level2State.drugsp1;

    public GameObject P1Manager;
    public GameObject P2Manager;
    public GameObject Meth;
    Animator anim;

    public GameObject[] flasks;
    public GameObject[] flasksForAnim;

    bool Triggered = false;

    //Part 1 variables

    public bool SolvedP1;
    bool ChangedBoolArray = true;

    //Part 2 variables
    public GameObject[] particlesystems;
    public GameObject[] particlesystems2;


    private void Start()
    {
        audiosource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        for (int i = 0; i < flasksForAnim.Length; i++)
        {
            flasksForAnim[i].SetActive(false);
        }
        anim = GetComponent<Animator>();
        P2Manager.SetActive(false);
        ChangedBoolArray = false;
        if (GameManager.staticLevelsCleared[2] == true)
        {
            level2State = Level2State.LevelComplete;
        }
    }

    void Update()
    {
        switch (level2State)
        {
            case Level2State.drugsp1:
                CheckSolveP1();
                break;
            case Level2State.drugsp2:
                CheckSolveP2();
                break;
            case Level2State.LevelComplete:
                LevelCleared();
                break;
        }

    }

    public void CheckSolveP1()
    {
        bool solve = true;

        if (P1Manager.GetComponent<DrugsPuzzleController>().Solved == false)
        {
            solve = false;
        }

        if (solve)
        {
            Destroy(P1Manager);
            ChangedBoolArray = true;
            SolvedP1 = true;
            P2Manager.SetActive(true);
            audiosource.clip = foundBottles;
            audiosource.Play();
            level2State = Level2State.drugsp2;
        }
    }
    public void CheckSolveP2()
    {
        bool solve = true;

        if (P2Manager.GetComponent<DrugsPuzzleController>().Solved == false)
        {
            solve = false;
        }

        if (solve)
        {
            Destroy(P2Manager);
            ChangedBoolArray = true;
            SolvedP1 = true;
            
            level2State = Level2State.LevelComplete;
            State2();
            GameManager.SetLevelAsComplete(2);
            Debug.Log(GameManager.staticLevelsCleared);
            ChangedBoolArray = true;
        }
    }

    public void State2()
    {
        if (!Triggered)
        {
            StartCoroutine(ActivateParticles());
            
        }
    }

    public void LevelCleared()
    {
        if (P1Manager != null)
        {
            Destroy(P1Manager);
        }
        if (P2Manager != null)
        {
            Destroy(P2Manager);
        }
        if (!ChangedBoolArray)
        {
            GameManager.SetLevelAsComplete(2);
            Debug.Log(GameManager.staticLevelsCleared);
            ChangedBoolArray = true;
        }
        
        anim.SetBool("FinishedLv", true);
    }

    public IEnumerator ActivateParticles()
    {
        Triggered = true;
        audiosource.clip = Solve;
        audiosource.Play();
        for (int i = 0; i < flasksForAnim.Length; i++)
        {
            flasksForAnim[i].SetActive(true);
        }
        anim.GetComponent<Animator>().SetBool("Trigger", true);

        yield return new WaitForSeconds(6);
        for (int i = 0; i < particlesystems.Length; i++)
        {
            particlesystems[i].SetActive(true);
        }
        yield return new WaitForSeconds(3);
        for (int i = 0; i < particlesystems2.Length; i++)
        {
            particlesystems2[i].SetActive(true);
        }
        anim.GetComponent<Animator>().SetBool("Trigger2", true);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < flasks.Length; i++)
        {
            flasks[i].SetActive(false);
        }
        for (int i = 0; i < flasks.Length; i++)
        {
            particlesystems[i].SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < flasks.Length; i++)
        {
            particlesystems2[i].SetActive(false);
            //flasks[i].GetComponent<Animator>().SetBool("Trigger3", true);
        }
        anim.GetComponent<Animator>().SetBool("Trigger3", true);
        yield return new WaitForSeconds(5);
        level2State = Level2State.LevelComplete;
        lift.GetComponent<Animator>().SetBool("DoorStateOpen", true);

        yield return new WaitForSeconds(7f);
        
    }

    public void ResetDrugsPuzzleAudio()
    {
        failCount++;
        if (failCount == 1)
        {
            audiosource.clip = Fail1;
            audiosource.Play();
            
        }
        else if (failCount == 5)
        {
            audiosource.clip = Fail2;
            audiosource.Play();
        }
    }
}
