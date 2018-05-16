using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Controller : MonoBehaviour {

    float t;
    float dur = 3;
    int i = 0;

    public GameObject[] level;
    public GameObject disconnectScene;
    public GameObject beforeDisconnectScene;
    public AudioClip disconnect;
    GameObject cam;

    public AudioSource radio;

    public AudioClip _radio;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        //GameManager.DisableAmbientMusic();
        //radio.clip = _radio;
        //radio.Play();
    }

    private void Start()
    {
        StartCoroutine(LevelSequence());
    }

    IEnumerator LevelSequence()
    {
        yield return new WaitForSeconds(5f);
        cam.transform.parent.GetComponent<Animator>().SetBool("Trigger", true);
        cam.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(15f);
        for (int j = 0; j < level.Length; j++)
        {
            level[j].SetActive(false);
        }
        beforeDisconnectScene.SetActive(true);
        yield return new WaitForSeconds(6f);
        disconnectScene.SetActive(true);
        radio.gameObject.SetActive(false);
        RenderSettings.fog = false;
        cam.gameObject.GetComponent<AudioSource>().clip = disconnect;
        cam.gameObject.GetComponent<AudioSource>().Play();
        StartCoroutine(SoundLoop());


    }

    public IEnumerator FadeOutLevelObjects()
    {
        Debug.Log("started");
        t = 0;
        while (level.Length > i)
        {
            yield return null;
            t += Time.deltaTime;
            if (t > dur)
            {
                if(dur > 1f)
                {
                    dur -= 0.666f;
                }
                level[i].SetActive(false);
                i++;
            }
        }
    }

    IEnumerator SoundLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(7f);
            cam.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
