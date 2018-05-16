using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.LoadScene("DontDestroy", LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
