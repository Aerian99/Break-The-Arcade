using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_Change : MonoBehaviour
{
    public SpriteRenderer actualGun;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                //other.gameObject.GetComponent<SpriteRenderer>().sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
                actualGun.sprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
                Destroy(gameObject);
            }
        }
    }
}
