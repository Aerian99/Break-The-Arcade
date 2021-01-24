﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound,
        shotgunSound,
        landingSound,
        yellowgunSound,
        purpleGunSound,
        dashSound,
        platformJump,
        gameOver,
        EnemyShoot,
        noAmmo,
        alienExplosion,
        openDoor,
        closeDoor,
        footStep,
        absorbSound,
        absorbBlip,
        hurt,
        powerup,
        radialEnemyHurt,
        radialEnemyDeath,
        patrolEnemyDeath,
        radialShoot,
        bossMusic1,
        bossMusic2,
        gameOverSong,
        dropSound,
        coinSound;

    public static AudioSource audioSrc;

    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jump");
        shotgunSound = Resources.Load<AudioClip>("shotgun");
        landingSound = Resources.Load<AudioClip>("landing");
        yellowgunSound = Resources.Load<AudioClip>("yellowGun");
        purpleGunSound = Resources.Load<AudioClip>("purpleGun");
        dashSound = Resources.Load<AudioClip>("dash");
        platformJump = Resources.Load<AudioClip>("platformJump");
        gameOver = Resources.Load<AudioClip>("gameOver");
        EnemyShoot = Resources.Load<AudioClip>("EnemyShoot");
        noAmmo = Resources.Load<AudioClip>("noAmmo");
        alienExplosion = Resources.Load<AudioClip>("alienExplosion");
        openDoor = Resources.Load<AudioClip>("openDoor");
        closeDoor = Resources.Load<AudioClip>("closeDoor");
        footStep = Resources.Load<AudioClip>("footStep");
        absorbSound = Resources.Load<AudioClip>("absorbSound");
        absorbBlip = Resources.Load<AudioClip>("absorbBlip");
        hurt = Resources.Load<AudioClip>("hurt");
        powerup = Resources.Load<AudioClip>("powerup");
        radialEnemyHurt = Resources.Load<AudioClip>("radialEnemyHurt");
        radialEnemyDeath = Resources.Load<AudioClip>("radialEnemyDeath");
        patrolEnemyDeath = Resources.Load<AudioClip>("patrolEnemyDeath");
        radialShoot = Resources.Load<AudioClip>("radialShoot");
        bossMusic1 = Resources.Load<AudioClip>("lvl1");
        bossMusic2 = Resources.Load<AudioClip>("lvl2");
        gameOverSong = Resources.Load<AudioClip>("sadChiptune");
        dropSound = Resources.Load<AudioClip>("dropSound");
        coinSound = Resources.Load<AudioClip>("coin2");
        audioSrc = GetComponent<AudioSource>();
        
    }


    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "shotgun":
                audioSrc.PlayOneShot(shotgunSound);
                break;
            case "landing":
                audioSrc.PlayOneShot(landingSound);
                break;
            case "yellowGun":
                audioSrc.PlayOneShot(yellowgunSound);
                break;
            case "purpleGun":
                audioSrc.PlayOneShot(purpleGunSound);
                break;
            case "dash":
                audioSrc.PlayOneShot(dashSound);
                break;
            case "platformJump":
                audioSrc.PlayOneShot(platformJump);
                break;
            case "gameOver":
                audioSrc.PlayOneShot(gameOver);
                break;
            case "EnemyShoot":
                audioSrc.PlayOneShot(EnemyShoot);
                break;
            case "noAmmo":
                audioSrc.PlayOneShot(noAmmo);
                break;
            case "alienExplosion":
                audioSrc.PlayOneShot(alienExplosion);
                break;
            case "openDoor":
                audioSrc.PlayOneShot(openDoor);
                break;
            case "closeDoor":
                audioSrc.PlayOneShot(closeDoor);
                break;
            case "footStep":
                audioSrc.PlayOneShot(footStep);
                break;
            case "absorbSound":
                audioSrc.PlayOneShot(absorbSound);
                break;
            case "absorbBlip":
                audioSrc.PlayOneShot(absorbBlip);
                break;
            case "hurt":
                audioSrc.PlayOneShot(hurt);
                break;
            case "powerup":
                audioSrc.PlayOneShot(powerup);
                break;
            case "radialEnemyHurt":
                audioSrc.PlayOneShot(radialEnemyHurt);
                break;
            case "radialEnemyDeath":
                audioSrc.PlayOneShot(radialEnemyDeath);
                break;
            case "patrolEnemyDeath":
                audioSrc.PlayOneShot(patrolEnemyDeath);
                break;
            case "BossMusic1":
                audioSrc.PlayOneShot(bossMusic1);
                break;
            case "BossMusic2":
                audioSrc.PlayOneShot(bossMusic2);
                break;
            case "gameOverSong":
                audioSrc.PlayOneShot(gameOverSong);
                break;
            case "dropSound":
                audioSrc.PlayOneShot(dropSound);
                break;
            case "coin":
                audioSrc.PlayOneShot(coinSound);
                break;
            default:
                break;
        }
    }

    public static void StopSound(string clip)
    {
        switch (clip)
        {
            case "gameOverSong":
                audioSrc.Stop();
                break;
            case "Play":
                audioSrc.Stop();
                break;
            default:
                break;
        }
    }

}