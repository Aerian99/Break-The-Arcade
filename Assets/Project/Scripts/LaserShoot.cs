﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserShoot : MonoBehaviour
{
    //private float distance;
    public static float damage;
    private bool startedShooting;
    
    // Particle components
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject startVFX;
    public GameObject endVFX;
    private List<ParticleSystem> particles = new List<ParticleSystem>();


    private float nextFrame;
    private float time;
    private float period;
    private float canShoot, maxShoot; 

    public GameObject reloadText;
    public GameObject noAmmoText;

    // Start is called before the first frame update
    void Start()
    {
        //distance = 100;
        startedShooting = false;
        damage = 3f;
        nextFrame = 0;
        time = 0;
        period = 0.5f;
        maxShoot = 1f;
        canShoot = maxShoot;
        FillLists();
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "PowerUpScene")
        {
            ChargeShoot();
        }
        else
        {
        //SHOOT
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                time = nextFrame;
                EnableLaser();
            }
            if (startedShooting && playerBehaviour.bulletsYellow > 0)
            {
                CheckFirstAbsorb();
                ScreenShake.shake = 1.5f;
                ScreenShake.canShake = true;
                UpdateLaser();
            }

            if (time >= nextFrame && startedShooting && !Input.GetKeyUp(KeyCode.Mouse0) &&
                playerBehaviour.bulletsYellow > 0)
            {
                nextFrame += period;
                playerBehaviour.bulletsYellow--;
                SoundManagerScript.PlaySound("yellowGun");
            }
            else if (time >= nextFrame && Input.GetKeyDown(KeyCode.Mouse0) && playerBehaviour.bulletsYellow > 0 &&
                     this.gameObject.activeInHierarchy == true && !startedShooting)
            {
                nextFrame += period;
                startedShooting = true;
                playerBehaviour.bulletsYellow--;
                SoundManagerScript.PlaySound("yellowGun");
                ScreenShake.shake = 1.5f;
                ScreenShake.canShake = true;
                CheckFirstAbsorb();
                //EnableLaser();
            }

            if ((Input.GetKeyUp(KeyCode.Mouse0) && startedShooting) || playerBehaviour.bulletsYellow == 0)
            {
                startedShooting = false;
                DisableLaser();
            }

            //POPUPS
            if (time >= nextFrame && Input.GetButton("Fire1") && playerBehaviour.bulletsYellow == 0 &&
                this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoYellow > 0)
            {
                reloadText.SetActive(true);
            }

            else if (time >= nextFrame && Input.GetButton("Fire1") && playerBehaviour.bulletsYellow == 0 &&
                     this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoYellow == 0)
            {
                noAmmoText.SetActive(true);
            }

            time += Time.deltaTime;
        }
    }

    void CheckFirstAbsorb()
    {
        if (Absorb_Gun.firstTimeAbsorb1)
        {
            Absorb_Gun.firstTimeAbsorb1 = false;
            Absorb_Gun.ammoFull1 = true;
        }
    }
    void EnableLaser()
    {
        lineRenderer.enabled = true;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }
    }

    void UpdateLaser()
    {
        Vector2 mousePos = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, (Vector2) firePoint.position);
        startVFX.transform.position = firePoint.position;
        
        lineRenderer.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2) transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2) firePoint.position, direction.normalized, direction.magnitude);
        
        if (hit && !hit.transform.CompareTag("Player"))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<AlienBehaviour>().laserDamage = true;
            }
            lineRenderer.SetPosition(1, hit.point);
        }
        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }

    void FillLists()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            ParticleSystem ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
        
        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            ParticleSystem ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
            {
                particles.Add(ps);
            }
        }
    }

    void ChargeShoot()
    {
        CheckFirstAbsorb();
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerBehaviour.bulletsYellow >= 3)
        {
            damage = 10f;
            startedShooting = true;
        }
        if (startedShooting)
        {
            canShoot -= Time.deltaTime;
            ScreenShake.shake = 1.5f;
            ScreenShake.canShake = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) && playerBehaviour.bulletsYellow >= 3 && startedShooting && canShoot <= 0)
        {
            startedShooting = false;
            playerBehaviour.bulletsYellow -= 3;
            EnableLaser();
            UpdateLaser();
            StartCoroutine(EraseLaser());
            canShoot = maxShoot;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && canShoot >= 0)
        {
            startedShooting = false;
            canShoot = maxShoot;
        }

        if (playerBehaviour.bulletsYellow < 3)
        {
            reloadText.SetActive(true);
        }
        else
        {
            reloadText.SetActive(false);
        }

        if (playerBehaviour.bulletsYellow < 3 && playerBehaviour.reservedAmmoYellow + playerBehaviour.bulletsYellow < 3)
        {
            noAmmoText.SetActive(true);
        }
        else
        {
            noAmmoText.SetActive(false);
        }

    }


    IEnumerator EraseLaser()
    {
        yield return new WaitForSeconds(0.5f);
        DisableLaser();
    }
}