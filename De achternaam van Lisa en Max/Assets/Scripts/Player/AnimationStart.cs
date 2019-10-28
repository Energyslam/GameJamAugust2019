using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStart : MonoBehaviour
{
    public static AnimationStart instance;
    public AnimationClip clip, stutter, stutterBig;
    public Animation animation;

    private void Start()
    {
        instance = this;
        animation = GetComponent<Animation>();
        animation.wrapMode = WrapMode.Once;
    }

    public void StartAnimation()
    {
        animation.AddClip(clip, "clip");
        animation.Play("clip");
    }
}
