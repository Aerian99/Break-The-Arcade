using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashCooldown : MonoBehaviour
{
    public Image cooldown;
    private float waitTime;
    void Start()
    {
        waitTime = 3f;
        cooldown.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown.fillAmount += 1.0f / waitTime * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            cooldown.fillAmount = 0f;
        }
    }
}
