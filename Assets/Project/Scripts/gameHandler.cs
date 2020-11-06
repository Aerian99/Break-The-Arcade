using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public camerBehaviour cameraFollow;
    public Transform playerTransform;
    void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }
}
