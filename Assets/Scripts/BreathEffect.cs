using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngineInternal;

public class BreathEffect : MonoBehaviour
{
    public float large;
    public float small;
    public float roundTime;

    private bool isBig;
    private bool NeedScale;
    private Tweener t;
    private Vector3 smallOne;
    private Vector3 BigOne;

    // Use this for initialization
    void Start()
    {
        isBig = true;
        NeedScale = false;
        smallOne = new Vector3(small, small, small);
        BigOne = new Vector3(large, large, large);
       
        AddScaleEvent();
    }

    void OnEnable()
    {
        isBig = true;
        NeedScale = false;
        smallOne = new Vector3(small, small, small);
        BigOne = new Vector3(large, large, large);

        AddScaleEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedScale)
        {
            NeedScale = false;
            isBig = !isBig;
            AddScaleEvent();
        }
    }

    void AddScaleEvent()
    {
        if (isBig)
        {
            t = transform.DOScale(smallOne, roundTime);
        }
        else
        {
            t = transform.DOScale(BigOne, roundTime);
        }

        t.onComplete = () =>
        {
            NeedScale = true;
        };
    }
}