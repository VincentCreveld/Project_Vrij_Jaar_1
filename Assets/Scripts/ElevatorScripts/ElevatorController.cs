using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorController : MonoBehaviour{

    public Camera elevatorCam;
    public enum SceneToLoad { lv1 = 1, lv2 = 2, lv3 = 3};
    public SceneToLoad sceneToLoad = SceneToLoad.lv1;

    public void LoadLevel()
    {
        SceneManager.LoadSceneAsync((int)sceneToLoad);
        elevatorCam.gameObject.SetActive(false);
    }

    public void ChangeSceneToLoad(int num)
    {
        Debug.Log("changing scene to load");
        switch (num)
        {
            case 1:
                sceneToLoad = SceneToLoad.lv1;
                break;
            case 2:
                sceneToLoad = SceneToLoad.lv2;
                break;
            case 3:
                sceneToLoad = SceneToLoad.lv3;
                break;
        }
    }
}
