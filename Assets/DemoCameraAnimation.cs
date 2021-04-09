using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCameraAnimation : MonoBehaviour
{
    public bool endCamAnimation;
    void Start()
    {
        endCamAnimation = false;
    }

    public void cameraAnimationEnd()
    {
        endCamAnimation = true;
    }
}
