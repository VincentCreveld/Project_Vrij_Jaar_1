using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightableObj : MonoBehaviour, IHighlightable
{
    public AnimationCurve _curve;
    Material mat;
    float timer;
    public float duration;

    public void Start()
    {
        mat = GetComponent<Renderer>().material;
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
