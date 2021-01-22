using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class absorbCooldown : MonoBehaviour
{
    public Image cooldown;
    public TextMeshProUGUI warning;
    //private bool coolingDown;
    public static bool coolFull;
    private float waitTime;
    private Color _initColor;
    private float incAbsorbSpeed;
    private float decAbsorbSpeed;

    public GameObject absorbZone;
    
    public Material defaultAbsorbZone;
    public Material orangeAbsorbZone;
    public Material redAbsorbZone;

    void Start()
    {
        _initColor = cooldown.color;
        waitTime = 3.0f;
        //coolingDown = true;
        coolFull = false;
        cooldown.fillAmount = 0f;
        incAbsorbSpeed = 1.0f;
        decAbsorbSpeed = 0.3f;
    }

    void Update()
    {
        if (Input.GetButton("Fire2") && coolFull == false && !playerBehaviour.isReloading)
        {
            cooldown.fillAmount += incAbsorbSpeed / waitTime * Time.deltaTime;
        }
        else
        {
            cooldown.fillAmount -= decAbsorbSpeed / waitTime * Time.deltaTime;
            //coolingDown = true;
        }

        if (cooldown.fillAmount >= 1f)
        {
            cooldown.color = new Color(255, 0, 0);
            coolFull = true;
        }
        else if (cooldown.fillAmount <= 0f)
        {
            cooldown.color = _initColor;
            //coolingDown = false;
            coolFull = false;
        }
        else if (cooldown.fillAmount < 0.3f && coolFull == false)
        {
            absorbZone.GetComponent<SpriteRenderer>().material = defaultAbsorbZone;
        }
        else if (cooldown.fillAmount > 0.3f && cooldown.fillAmount < 0.8f && coolFull == false)
        {
            absorbZone.GetComponent<SpriteRenderer>().material = orangeAbsorbZone;
        }
        else if (cooldown.fillAmount > 0.8f && coolFull == false)
        {
            absorbZone.GetComponent<SpriteRenderer>().material = redAbsorbZone;
            warning.gameObject.SetActive(true);
        }
        else
        {
            warning.gameObject.SetActive(false);
        }
    }
}