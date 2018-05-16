using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightableList : MonoBehaviour, IHighlightable {

    public AnimationCurve _curve;
    public bool PullMatFromObj;
    public GameObject[] ObjToPullMat;
    Material[] mat;
    float timer;
    public float duration;

    public void Start()
    {
        mat = new Material[ObjToPullMat.Length];

        for (int i = 0; i < ObjToPullMat.Length; i++)
        {
            mat[i] = ObjToPullMat[i].GetComponent<MeshRenderer>().material;
        }

        /*
        if (PullMatFromObj == false)
        {
            if (transform.GetComponent<MeshRenderer>().material != null)
            {
                ObjToPullMat = gameObject;
            }
        }

        mat = ObjToPullMat.GetComponent<MeshRenderer>().material;
        */
    }

    public Material ReturnMatGO()
    {
        return mat[0];
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
        for (int i = 0; i < mat.Length; i++)
        {
            mat[i].SetColor("_EmissionColor", tempColor);
        }
        
    }
    public void DeHighlight()
    {
        Color tempColor = new Color(0.0f, 0, 0, 1);
        for (int i = 0; i < mat.Length; i++)
        {
            mat[i].SetColor("_EmissionColor", tempColor);
        }
        timer = 0;
    }

}
