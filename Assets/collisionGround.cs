using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(-1, 1);
        Vector2 movement = new Vector2(rand, 0);
        gameObject.GetComponent<Rigidbody2D>().AddForce(movement);
        Destroy(gameObject, 3f);
    }



    // Update is called once per frame
   

}
