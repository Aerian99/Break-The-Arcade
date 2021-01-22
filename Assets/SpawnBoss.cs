﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBoss : MonoBehaviour
{
    public GameObject boss;
    public GameObject imageBoss;
    public GameObject lifeBoss;
    public Vector3 m_position;
    public GameObject audio;
    public AudioClip musica;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audio.GetComponent<AudioSource>().clip = musica;
            imageBoss.SetActive(true);
            lifeBoss.SetActive(true);
            Instantiate(boss, m_position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
