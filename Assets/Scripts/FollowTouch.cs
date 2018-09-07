using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTouch : MonoBehaviour
{
    public int index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.touchCount > index)
	    {
	        gameObject.GetComponent<RectTransform>().position = Input.touches[index].position;
	    }
	}
}
