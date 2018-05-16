using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Lv3Liftdeur : MonoBehaviour, IInteractable
{

    public GameObject Player;
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
        StartCoroutine(GoToElevator());
    }

    public IEnumerator GoToElevator()
    {
        StartCoroutine(playControl.FadeOut());
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync("Elevator34");
    }
}
