using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{
    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject startVFX;
    public GameObject endVFX;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    //private float distance;
    public LineRenderer line;
    public Transform l_transform;
    public static float damage;
    private bool startedShooting;

    private float nextFrame;
    private float time;
    private float period;

    public GameObject reloadText;
    public GameObject noAmmoText;

    // Start is called before the first frame update
    void Start()
    {
        l_transform = GetComponent<Transform>();
        //distance = 100;
        startedShooting = false;
        damage = 3f;
        nextFrame = 0;
        time = 0;
        period = 0.5f;
    }

    private void Update()
    {
        //SHOOT
        if (Input.GetKeyDown(KeyCode.Mouse0))
            time = nextFrame;
        if (startedShooting && playerBehaviour.bulletsYellow > 0)
        {
            Shoot();
            ScreenShake.shake = 1.5f;
            ScreenShake.canShake = true;
        }
        if (time >= nextFrame && startedShooting && !Input.GetKeyUp(KeyCode.Mouse0) && playerBehaviour.bulletsYellow > 0)
        {  
            nextFrame += period;
            playerBehaviour.bulletsYellow--;
            SoundManagerScript.PlaySound("yellowGun");
        }
        else if(time >= nextFrame && Input.GetKeyDown(KeyCode.Mouse0) && playerBehaviour.bulletsYellow > 0 && this.gameObject.activeInHierarchy == true && !startedShooting)
        {
            nextFrame += period;
            startedShooting = true;
            playerBehaviour.bulletsYellow--;
            Shoot();
            SoundManagerScript.PlaySound("yellowGun");
            ScreenShake.shake = 1.5f;
            ScreenShake.canShake = true;
        }

        if((Input.GetKeyUp(KeyCode.Mouse0) && startedShooting) ||  playerBehaviour.bulletsYellow == 0)
        {
            line.enabled = false;
            startedShooting = false;
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

    void EnableLaser()
    {
        if (Absorb_Gun.firstTimeAbsorb1)
        {
            Absorb_Gun.firstTimeAbsorb1 = false;
            Absorb_Gun.ammoFull1 = true;
        }

        line.enabled = true;

        if (Physics2D.Raycast(l_transform.position, transform.right))
        {
            particles[i].Play();
        }
    }

    void UpdateLaser()
    {
        Vector2 mousePos = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, (Vector2) firePoint.position);
        startVFX.transform.position = (Vector2) firePoint.position;
        
        lineRenderer.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2) transform.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2) firePoint.position, direction.normalized, direction.magnitude);

        if (hit && !hit.transform.CompareTag("Player"))
        {
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
}