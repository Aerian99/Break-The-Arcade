using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class hitDamagePopUp : MonoBehaviour
{
    private Vector3 randomPopup = new Vector3(0.75f, 0.75f, 0f);
    void Start()
    {
        // Randomizamos la aparición del Pop Up
        transform.localPosition += new Vector3(Random.Range(-randomPopup.x, randomPopup.x),
            Random.Range(-randomPopup.y, randomPopup.y), randomPopup.z);
    }
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
