using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGController : MonoBehaviour
{
    // Use this for initialization
    public MovieTexture IntroMovieTexture;
    public MovieTexture LoopMovieTexture;

    void Start()
    {
        SwitchToIntro();
    }

    public void SwitchToIntro()
    {
        LoopMovieTexture.Stop();
        IntroMovieTexture.loop = true;
        IntroMovieTexture.Play();
        RawImage ri = gameObject.GetComponent<RawImage>();
        ri.texture = IntroMovieTexture;
    }

    public void SwitchToLoop()
    {
        IntroMovieTexture.Stop();
        LoopMovieTexture.loop = true;
        LoopMovieTexture.Play();
        RawImage ri = gameObject.GetComponent<RawImage>();
        ri.texture = LoopMovieTexture;
    }
}