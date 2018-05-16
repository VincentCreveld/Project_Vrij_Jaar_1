using UnityEngine;
public class ElevatorButton : MonoBehaviour, IInteractable {
    public GameObject ElevatorManager;
    ElevatorController elevatorController;
    public int SceneToLoad;

    void Start()
    {
        elevatorController = ElevatorManager.GetComponent<ElevatorController>();
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
        elevatorController.ChangeSceneToLoad(SceneToLoad);
    }
}
