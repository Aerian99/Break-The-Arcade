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
    public GameObject noAmmoText, reloadText;
    public static int currentPos;
    

    public GameObject purpleReloaderUI, yellowReloaderUI, redReloaderUI;

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
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Rueda arriba
        {
            currentPos--;
            if (noAmmoText.activeInHierarchy)
            {
                noAmmoText.SetActive(false);
            }
            if (reloadText.activeInHierarchy)
                reloadText.SetActive(false);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Rueda abajo
        {
            currentPos++;
            if (noAmmoText.activeInHierarchy)
            {
                noAmmoText.SetActive(false);
            }
            if (reloadText.activeInHierarchy)
                reloadText.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentPos = 0;
            if (reloadText.activeInHierarchy)
                reloadText.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentPos = 1;
            if (reloadText.activeInHierarchy)
                reloadText.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentPos = 2;
            if (reloadText.activeInHierarchy)
                reloadText.SetActive(false);
        }
        WeaponSelector();
    }

    void WeaponSelector()
    {
        if (currentPos == 0)
        {
            purpleGun.SetActive(true);
            yellowGun.SetActive(false);
            redGun.SetActive(false);

            // GUN COLOR IDENTIFIER
            purpleGunUI.gameObject.SetActive(true);
            yellowGunUI.gameObject.SetActive(false);
            redGunUI.gameObject.SetActive(false);

            // RELOADER UI
            purpleReloaderUI.SetActive(true);
            yellowReloaderUI.SetActive(false);
            redReloaderUI.SetActive(false);
            
        }
        else if (currentPos == 1)
        {
            purpleGun.SetActive(false);
            yellowGun.SetActive(true);
            redGun.SetActive(false);
            
            // GUN COLOR IDENTIFIER
            purpleGunUI.gameObject.SetActive(false);
            yellowGunUI.gameObject.SetActive(true);
            redGunUI.gameObject.SetActive(false);
            
            // RELOADER UI
            purpleReloaderUI.SetActive(false);
            yellowReloaderUI.SetActive(true);
            redReloaderUI.SetActive(false);
        }

        else if (currentPos == 2)
        {
            purpleGun.SetActive(false);
            yellowGun.SetActive(false);
            redGun.SetActive(true);

            // GUN COLOR IDENTIFIER
            purpleGunUI.gameObject.SetActive(false);
            yellowGunUI.gameObject.SetActive(false);
            redGunUI.gameObject.SetActive(true);
            
            // RELOADER UI
            purpleReloaderUI.SetActive(false);
            yellowReloaderUI.SetActive(false);
            redReloaderUI.SetActive(true);
        }

        // ABSORB GUN ZONE
        if (Input.GetButton("Fire2") && absorbCooldown.coolFull == false /*&& !playerBehaviour.weaponMenuUp*/)
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