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
    public GameObject noAmmoText;
    public static int currentPos;

    private Transform gunSelector;
    void Start()
    {
        purpleGun = transform.GetChild(0).gameObject;
        absorbGun = transform.GetChild(1).gameObject;
        yellowGun = transform.GetChild(2).gameObject;
        redGun = transform.GetChild(3).gameObject;
        gunSelector = GameObject.FindWithTag("gunSelector").transform;
        currentPos = 0;
    }

    void Update()
    {
        WeaponSelector();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            currentPos--;
            //if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            currentPos++;
            //if (noAmmoText.activeInHierarchy) noAmmoText.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) currentPos = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) currentPos = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) currentPos = 2;
    }

    void WeaponSelector()
    {
        if (currentPos == 0)
        {
            purpleGun.SetActive(true);
            redGun.SetActive(false);

            // GUN COLOR IDENTIFIER
            purpleGunUI.color = new Color(purpleGunUI.color.r, purpleGunUI.color.g, purpleGunUI.color.b, 1f);
            yellowGunUI.color = new Color(yellowGunUI.color.r, yellowGunUI.color.g, yellowGunUI.color.b, 0.3176471f);
            redGunUI.color = new Color(redGunUI.color.r, redGunUI.color.g, redGunUI.color.b, 0.3176471f);

            // GUN SELECTOR
            gunSelector.position = new Vector3(purpleGunUI.transform.position.x, purpleGunUI.transform.position.y + 0.02f, purpleGunUI.transform.position.z);
        }
        else if (currentPos == 1)
        {
            purpleGun.SetActive(false);
            yellowGun.SetActive(true);
            redGun.SetActive(false);
            // GUN COLOR IDENTIFIER
            purpleGunUI.color = new Color(purpleGunUI.color.r, purpleGunUI.color.g, purpleGunUI.color.b, 0.3176471f);
            yellowGunUI.color = new Color(yellowGunUI.color.r, yellowGunUI.color.g, yellowGunUI.color.b, 1f);
            redGunUI.color = new Color(redGunUI.color.r, redGunUI.color.g, redGunUI.color.b, 0.3176471f);
            
            // GUN SELECTOR
            gunSelector.position = new Vector3(yellowGunUI.transform.position.x, yellowGunUI.transform.position.y + 0.02f, yellowGunUI.transform.position.z);
        }

        else if (currentPos == 2)
        {
            purpleGun.SetActive(false);
            yellowGun.SetActive(false);
            redGun.SetActive(true);

            // GUN COLOR IDENTIFIER
            purpleGunUI.color = new Color(purpleGunUI.color.r, purpleGunUI.color.g, purpleGunUI.color.b, 0.3176471f);
            yellowGunUI.color = new Color(yellowGunUI.color.r, yellowGunUI.color.g, yellowGunUI.color.b, 0.3176471f);
            redGunUI.color = new Color(redGunUI.color.r, redGunUI.color.g, redGunUI.color.b, 1f);
            
            // GUN SELECTOR
            gunSelector.position = new Vector3(redGunUI.transform.position.x, redGunUI.transform.position.y + 0.04f, redGunUI.transform.position.z);
        }



        // ABSORB GUN ZONE
        if (Input.GetButton("Fire2") && absorbCooldown.coolFull == false)
        {
            absorbGun.SetActive(true);
            purpleGun.SetActive(false);
            yellowGun.SetActive(false);
            redGun.SetActive(false);

        }
        else if (currentPos == 0)
        {
            purpleGun.SetActive(true);
            absorbGun.SetActive(false);
            yellowGun.SetActive(false);
            redGun.SetActive(false);
        }
        else if (currentPos == 1)
        {
            purpleGun.SetActive(false);
            absorbGun.SetActive(false);
            yellowGun.SetActive(true);
            redGun.SetActive(false);
        }
        else if (currentPos == 2)
        {
            purpleGun.SetActive(false);
            absorbGun.SetActive(false);
            yellowGun.SetActive(false);
            redGun.SetActive(true);
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


