using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BreathEffectArrow : MonoBehaviour
{
    public int length;
    public float roundTime;

    private bool isRight;
    private bool NeedMove;

    private Tweener t;

    // Use this for initialization
    void Start()
    {
        isRight = false;
        NeedMove = false;
        AddMoveEvent();
    }

    void OnEnable()
    {
        isRight = false;
        NeedMove = false;

        AddMoveEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedMove)
        {
            NeedMove = false;
            isRight = !isRight;
            AddMoveEvent();
        }
    }

    void AddMoveEvent()
    {
        if (isRight)
        {
            Vector3 target = transform.position - transform.right *length;
            t = transform.DOMove(target, roundTime);
        }
        else
        {
            Vector3 target = transform.position + transform.right * length;
            t = transform.DOMove(target, roundTime);
        }

        t.onComplete = () =>
        {
            NeedMove = true;
        };
    }
}
