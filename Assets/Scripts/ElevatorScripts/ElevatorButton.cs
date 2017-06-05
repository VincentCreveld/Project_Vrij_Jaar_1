using UnityEngine;

public class ElevatorButton : MonoBehaviour, IInteractable {
    ElevatorController elevatorController;
    public int SceneToLoad;

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
        elevatorController.ChangeSceneToLoad(SceneToLoad);
    }
}
