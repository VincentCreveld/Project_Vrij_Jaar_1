using UnityEngine;

public class ParentScript : MonoBehaviour, IInteractable {

    public GameObject levelControll;

    public enum Level2 { pillbox, parents }
    public Level2 level2 = Level2.pillbox;
    public void CheckInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        switch (level2)
        {
            case (Level2.pillbox):
                levelControll.GetComponent<Level1Controller>().PillboxPickedUp = true;
                Destroy(gameObject);
                break;
            case (Level2.parents):
                levelControll.GetComponent<Level1Controller>().PillsInBottle = true;
                break;
        }
    }
}
