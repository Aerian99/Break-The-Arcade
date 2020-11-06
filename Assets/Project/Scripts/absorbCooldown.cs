using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class absorbCooldown : MonoBehaviour
{
    public Image cooldown;
    public TextMeshProUGUI warning;
    private bool coolingDown;
    public static bool coolFull;
    private float waitTime;
    private Color _initColor;

    void Start()
    {
        _initColor = cooldown.color;
        waitTime = 3.0f;
        coolingDown = true;
        coolFull = false;
        cooldown.fillAmount = 0f;
    }

    void Update()
    {
        if (Input.GetButton("Fire2") && coolFull == false)
        {
            cooldown.fillAmount += 1.0f / waitTime * Time.deltaTime;
        }
        else
        {
            cooldown.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            coolingDown = true;

        }

        if (cooldown.fillAmount == 1f)
        {
            cooldown.color = new Color(255, 0, 0);
            coolFull = true;

        }
        else if (cooldown.fillAmount == 0f)
        {
            cooldown.color = _initColor;
            coolingDown = false;
            coolFull = false;
        }
        else if (cooldown.fillAmount > 0.8f && coolFull == false)
        {
            warning.gameObject.SetActive(true);
        }
        else
        {
            warning.gameObject.SetActive(false);
        }
    }
}
