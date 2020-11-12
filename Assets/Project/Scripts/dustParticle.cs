using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class dustParticle : MonoBehaviour
{
    public GameObject dustEffect;
    private GameObject dustGO;
    private bool hit;

    void Start()
    {
        hit = false;
    }

    void Update()
    {
        Destroy(dustGO, 0.3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Platform" || other.gameObject.tag == "JumpPlatform" && hit == true)
        {
            dustGO = Instantiate(dustEffect, this.transform.position, dustEffect.transform.rotation);
        }
        hit = true;
    }
}