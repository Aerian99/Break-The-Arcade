﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHandler : MonoBehaviour
{
    public camerBehaviour cameraFollow;
    public Transform playerTransform;
    void Start()
    {
        cameraFollow.Setup(() => new Vector3(playerTransform.position.x, playerTransform.position.y + 3f, playerTransform.position.z));
    }
}
