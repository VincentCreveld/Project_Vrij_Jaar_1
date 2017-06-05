using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorDoor : MonoBehaviour, IInteractable {

    public enum ToBack { to, back}
    public ToBack toBack;
    ElevatorController elevatorController;

    void Start()
    {
        elevatorController = transform.parent.GetComponent<ElevatorController>();
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
                elevatorController.LoadLevel();
                break;
            case 1:
                SceneManager.LoadSceneAsync(0);
                break;
    }
    }
}
