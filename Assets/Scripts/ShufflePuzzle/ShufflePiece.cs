using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShufflePiece : MonoBehaviour, IInteractable, IHighlightable {

    public Text displayText;
    public AnimationCurve _curve;
    public bool PullMatFromObj;
    public GameObject ObjToPullMat;
    Material mat;
    float timer;
    public float duration;
    public int id;
    public int originalId;
    public bool selected;
    ShufflePuzzle shufflePuzzle;

	void Start ()
    {
        shufflePuzzle = transform.parent.GetComponent<ShufflePuzzle>();
        if (PullMatFromObj == false)
        {
            if (transform.GetComponent<MeshRenderer>().material != null)
            {
                ObjToPullMat = gameObject;
            }
        }

        mat = ObjToPullMat.GetComponent<MeshRenderer>().material;

    }

    void Update()
    {
        if (selected)
        {
            Highlight();
        }
        if (displayText != null)
        {
            displayText.text = originalId.ToString();
        }
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
        if (!shufflePuzzle.Solved)
        {
            Debug.Log("Pressed");
            if (shufflePuzzle.arrayId1 == null)
            {
                SetShuffleArrayPos1();
                selected = true;
            }
            else if (shufflePuzzle.arrayId2 == null)
            {
                selected = true;
                SetShuffleArrayPos2();
            }
        }
    }

    void SetShuffleArrayPos1()
    {
        shufflePuzzle.arrayId1 = gameObject;
    }

    void SetShuffleArrayPos2()
    {
        shufflePuzzle.arrayId2 = gameObject;
    }

    public Material ReturnMatGO()
    {
        return mat;
    }

    public void Highlight()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            timer -= duration;
        }
        float curveValue = _curve.Evaluate(timer);
        Color tempColor = new Color(curveValue, 0, 0, 1);
        mat.SetColor("_EmissionColor", tempColor);
    }
    public void DeHighlight()
    {
        
        Color tempColor = new Color(0.0f, 0, 0, 1);
        mat.SetColor("_EmissionColor", tempColor);
        timer = 0;
    }
}
