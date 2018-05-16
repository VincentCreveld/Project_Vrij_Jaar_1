
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static bool canPlayClip1Elevator = false;
    public static bool canPlayClip2Elevator = false;
    public static bool canPlayClip3Elevator = false;
    public static bool Clip1ElevatorPlayed = false;
    public static bool Clip2ElevatorPlayed = false;
    public static bool Clip3ElevatorPlayed = false;
    public static bool[] staticLevelsCleared;
    public enum Levels {elevator, lv1, lv2, lv3, lv4}
    public static Levels levels;

    static AudioSource audiosource;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Start ()
    {
        audiosource = GetComponent<AudioSource>();
        staticLevelsCleared = new bool[5];
        for (int i = 0; i < staticLevelsCleared.Length; i++)
        {
            staticLevelsCleared[i] = false;
        }
        staticLevelsCleared[(int)Levels.elevator] = true;
	}

    public static void SetLevelAsComplete(int num)
    {
        staticLevelsCleared[num] = true;
    }

    public static void DisableAmbientMusic()
    {
        audiosource.Stop();
    }
}
