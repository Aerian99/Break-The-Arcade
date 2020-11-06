using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recognizeGun : MonoBehaviour
{
    private Sprite actualGun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        actualGun = this.gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
