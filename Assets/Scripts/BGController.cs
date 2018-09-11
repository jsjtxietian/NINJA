using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGController : MonoBehaviour
{
    // Use this for initialization
    public MovieTexture movTexture;

    void Start()
    {
        movTexture.loop = true;
        movTexture.Play();
        RawImage ri = gameObject.GetComponent<RawImage>();
        ri.texture = movTexture;
    }

    // Update is called once per frame
    void Update()
    {
    }
}