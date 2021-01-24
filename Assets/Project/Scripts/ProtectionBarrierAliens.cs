using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionBarrierAliens : MonoBehaviour
{

    private int timesHitted;
    [HideInInspector]public bool hitted;
    // Start is called before the first frame update
    void Start()
    {
        timesHitted = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitted)
        {
            timesHitted++;
            hitted = false;
        }
        if (timesHitted >= 6 && timesHitted < 12)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(235,92,0,255);
            }
        }
        else if (timesHitted >= 12 && timesHitted < 18)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else if (timesHitted > 24)
            Destroy(this.gameObject);
    }
}
