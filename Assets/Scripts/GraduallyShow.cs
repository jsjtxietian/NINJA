using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GraduallyShow : MonoBehaviour
{
    public float WaitTime;

    void OnEnable()
    {
        gameObject.GetComponent<Image>().DOFade(1f, WaitTime);
    }
	
	void Update () {
		
	}
}
