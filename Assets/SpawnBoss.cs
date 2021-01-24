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
            SoundManagerScript.PlaySound("BossMusic");
            GameObject.Find("Music").GetComponent<AudioSource>().Stop();
            GameObject.Find("Character_Sprite_Sheet_0").GetComponent<ThrowAmmo>().activeBoss = true;
            GameObject.Find("Character_Sprite_Sheet_1").GetComponent<ThrowAmmo>().activeBoss = true;
            imageBoss.SetActive(true);
            lifeBoss.SetActive(true);
            Instantiate(boss, m_position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
