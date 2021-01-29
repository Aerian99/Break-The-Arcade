using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionBossKnight : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Boss")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isDashing", false);
            collision.gameObject.GetComponent<BossKhightBehaviour>().isDashing = false;
        }
    }
}
