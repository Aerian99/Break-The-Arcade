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

    private void Start()
    {
        FillLists();
        DisableLaser();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            EnableLaser();
        }

        if (Input.GetButton("Fire1"))
        {
            UpdateLaser();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            DisableLaser();
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