using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class handController : MonoBehaviour
{
    private GameObject purpleGun;
    public Image purpleGunUI;
    private GameObject yellowGun;

    public Image yellowGunUI;
    private GameObject redGun;
    public Image redGunUI;

    private GameObject absorbGun;
    private int currentPos;
    void Start()
    {
        purpleGun = transform.GetChild(0).gameObject;
        absorbGun = transform.GetChild(1).gameObject;
        yellowGun = transform.GetChild(2).gameObject;
        redGun = transform.GetChild(3).gameObject;
        currentPos = 0;
    }

    void Update()
    {
        WeaponSelector();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentPos--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            currentPos++;
        }
    }

    void WeaponSelector()
    {
        if (currentPos == 0)
        {
            purpleGun.SetActive(true);
            redGun.SetActive(false);
            //absorbGun.SetActive(false);

            // GUN COLOR IDENTIFIER
            purpleGunUI.color = new Color(purpleGunUI.color.r, purpleGunUI.color.g, purpleGunUI.color.b, 1f);
            redGunUI.color = new Color(redGunUI.color.r, redGunUI.color.g, redGunUI.color.b, 0.3176471f);
            yellowGunUI.color = new Color(yellowGunUI.color.r, yellowGunUI.color.g, yellowGunUI.color.b, 0.3176471f);
        }
        else if (currentPos == 1)
        {
            purpleGun.SetActive(false);
            redGun.SetActive(true);
            //absorbGun.SetActive(false);

            // GUN COLOR IDENTIFIER
            yellowGunUI.color = new Color(yellowGunUI.color.r, yellowGunUI.color.g, yellowGunUI.color.b, 1f);
            purpleGunUI.color = new Color(purpleGunUI.color.r, purpleGunUI.color.g, purpleGunUI.color.b, 0.3176471f);
            redGunUI.color = new Color(redGunUI.color.r, redGunUI.color.g, redGunUI.color.b, 0.3176471f);
        }

        else if (currentPos == 2)
        {
            purpleGun.SetActive(false);
            redGun.SetActive(true);
            //absorbGun.SetActive(false);

            // GUN COLOR IDENTIFIER
            purpleGunUI.color = new Color(purpleGunUI.color.r, purpleGunUI.color.g, purpleGunUI.color.b, 0.3176471f);
            yellowGunUI.color = new Color(yellowGunUI.color.r, yellowGunUI.color.g, yellowGunUI.color.b, 0.3176471f);
            redGunUI.color = new Color(redGunUI.color.r, redGunUI.color.g, redGunUI.color.b, 1f);
        }



        // ABSORB GUN ZONE
        if (Input.GetButton("Fire2") && absorbCooldown.coolFull == false)
        {
            absorbGun.SetActive(true);
            purpleGun.SetActive(false);
            redGun.SetActive(false);
            yellowGun.SetActive(false);
        }
        else if (currentPos == 0)
        {
            purpleGun.SetActive(true);
            redGun.SetActive(false);
            absorbGun.SetActive(false);
            yellowGun.SetActive(false);
        }
        else if (currentPos == 1)
        {
            purpleGun.SetActive(false);
            yellowGun.SetActive(true);
            redGun.SetActive(false);
            absorbGun.SetActive(false);
        }
        else if (currentPos == 2)
        {
            purpleGun.SetActive(false);
            yellowGun.SetActive(false);
            redGun.SetActive(true);
            absorbGun.SetActive(false);
        }

        if (currentPos == 3)
        {
            currentPos = 0;
        }
        else if (currentPos == -1)
        {
            currentPos = 2;
        }
    }
}


