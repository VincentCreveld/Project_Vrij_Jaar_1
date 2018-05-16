using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ElevatorDoor : MonoBehaviour, IInteractable {

    public GameObject Player;
    public enum ToBack { to, back}
    public ToBack toBack;
    public ElevatorController elevatorController;
    PlayerController playControl;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera");
        playControl = Player.GetComponent<PlayerController>();
    }

    public void CheckInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        switch ((int)toBack)
        {
            case 0:
                StartCoroutine(LeaveLevel());
                break;
            case 1:
                StartCoroutine(GoToElevator());
                break;
        }
    }

    public IEnumerator LeaveLevel()
    {
        StartCoroutine(playControl.FadeOut());
        yield return new WaitForSeconds(3);
        elevatorController.LoadLevel();
    }

    public IEnumerator GoToElevator()
    {
        StartCoroutine(playControl.FadeOut());
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("Elevator");
    }
}
