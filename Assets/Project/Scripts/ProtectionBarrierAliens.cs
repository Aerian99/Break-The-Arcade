using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionBarrierAliens : MonoBehaviour
{

    private int timesHitted;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        timesHitted = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timesHitted >= 6 && timesHitted < 12)
            GetComponent<SpriteRenderer>().color = new Color(235,92,0,255);
        else if (timesHitted >= 12 && timesHitted < 18)
            GetComponent<SpriteRenderer>().color = Color.red;
        else if (timesHitted > 24)
            Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AlienAttack")
        {
            timesHitted++;
        }
    }
}
