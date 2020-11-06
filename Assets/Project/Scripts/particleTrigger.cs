using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleTrigger : MonoBehaviour
{
    private ParticleSystem part;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag);
    }
}
