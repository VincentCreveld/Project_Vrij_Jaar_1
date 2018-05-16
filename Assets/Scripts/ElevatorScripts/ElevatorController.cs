using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElevatorController : MonoBehaviour{

    public Text ElevatorUI;
    public Animator ElevatorDoorAnim;
    public bool DoorOpen = false;
    public GameObject ElevatorDoor;

    public Camera elevatorCam;
    public GameObject objToPullArray;
    public GameObject[] objs;
    public enum SceneToLoad {lv1 = 2, lv2 = 3, lv3 = 4, lv4 = 5};
    public SceneToLoad sceneToLoad = SceneToLoad.lv1;

    public void Start()
    {
        objs = new GameObject[4];
        for (int i = 0; i < objToPullArray.transform.childCount; i++)
        { 
            objs[i] = objToPullArray.transform.GetChild(i).gameObject;
        }
    }

    public void Update()
    {
        for (int i = 0; i < objToPullArray.transform.childCount; i++)
        {
            if (GameManager.staticLevelsCleared[i] == false)
            {
                Destroy(objs[i]);
            }
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync((int)sceneToLoad);
        //elevatorCam.gameObject.SetActive(false);
    }

    public void ChangeSceneToLoad(int num)
    {
        Debug.Log("changing scene to load");
        switch (num)
        {
            case 1:
                sceneToLoad = SceneToLoad.lv1;
                ButtonPressed();
                break;
            case 2:
                sceneToLoad = SceneToLoad.lv2;
                ButtonPressed();
                break;
            case 3:
                sceneToLoad = SceneToLoad.lv3;
                ButtonPressed();
                break;
            case 4:
                sceneToLoad = SceneToLoad.lv4;
                ButtonPressed();
                break;
        }
    }

    public void ButtonPressed()
    {
        if (DoorOpen)
        {
            StartCoroutine(ElevatorSequence());
        }
        else
        {
            StartCoroutine(OpenDoor());
        }
    }

    public IEnumerator ElevatorSequence()
    {
        StartCoroutine(CloseDoor());
        yield return new WaitForSeconds(7f);
        StartCoroutine(OpenDoor());
    }

    public IEnumerator OpenDoor()
    {
        int value = (int)sceneToLoad-1;
        ElevatorDoor.GetComponent<Collider>().enabled = false;
        ElevatorDoorAnim.SetBool("DoorStateOpen", true);
        yield return new WaitForSeconds(2f);
        ElevatorUI.text = "-" + value.ToString();
        ElevatorDoor.GetComponent<Collider>().enabled = true;
        DoorOpen = true;
    }

    public IEnumerator CloseDoor()
    {
        ElevatorDoor.GetComponent<Collider>().enabled = false;
        ElevatorDoorAnim.SetBool("DoorStateOpen", false);
        yield return new WaitForSeconds(2f);
        DoorOpen = false;
    }

}
