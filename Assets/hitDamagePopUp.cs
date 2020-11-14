using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class hitDamagePopUp : MonoBehaviour
{
    private Vector3 randomPopup = new Vector3(0.75f, 0.75f, 0f);
    void Start()
    {
        // Destruimos el popUp pasados 0.5 segundos.
        Destroy(this.gameObject, 0.5f);
        
        // Randomizamos la aparición del Pop Up
        transform.localPosition += new Vector3(Random.Range(-randomPopup.x, randomPopup.x),
            Random.Range(-randomPopup.y, randomPopup.y),
            Random.Range(-randomPopup.z, randomPopup.z));
    }
}
