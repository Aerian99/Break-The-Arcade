using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaserShoot : MonoBehaviour
{
    public Sprite green, blue;
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
    private LayerMask hittableMasK, hittableMask2;


    private float nextFrame;
    private float time;
    private float period;
    private float canShoot, maxShoot;

    //public GameObject reloadText;
    public GameObject noAmmoText;
    private float maxCdAmmo, cdAmmo;

    public GameObject hitDamagePopUp;
    [HideInInspector] public float bulletForce = 5f;

    public GameObject cursor;
    private GameObject player, gameController;

    public bool greenPowerUp, bluePowerUp;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("gameController");
        hittableMasK = LayerMask.GetMask("Enemy", "Barril", "BarrilExplosivo", "Platforms", "BurstEnemy");
        //distance = 100;
        startedShooting = false;
        bulletForce = 3f + GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.damageYellow;
        nextFrame = 0;
        time = 0;
        period = 0.1f;
        maxShoot = 1f;
        canShoot = maxShoot;
        maxCdAmmo = 1.1f;
        cdAmmo = 0.0f;
        player = GameObject.FindGameObjectWithTag("Player");
        FillLists();
        DisableLaser();
       if(gameController.GetComponent<GameController>().playerCaracteristics.LaserGreen)
       {
            greenPowerUp = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = green;
            bulletForce = 7 + GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.damageYellow;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<LineRenderer>().materials[0].SetColor("Color_", new Color(0, 1 * 4.5f, 0));
            //START VFX
            gameObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
            gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);

            //END FVX
            gameObject.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
            gameObject.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
        }
       if (gameController.GetComponent<GameController>().playerCaracteristics.LaserBlue)
       {
            bluePowerUp = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = blue;
            bulletForce = 11 + GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.damageYellow;
            gameObject.transform.GetChild(1).GetChild(0).GetComponent<LineRenderer>().materials[0].SetColor("Color_", new Color(0, 0, 1 * 4.5f));
            //START VFX
            gameObject.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
            gameObject.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);

            //END FVX
            gameObject.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
            gameObject.transform.GetChild(1).GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bulletForce = GameObject.FindGameObjectWithTag("gameController").GetComponent<GameController>().playerCaracteristics.damageYellow;
        //SHOOT
        if (Input.GetKeyDown(KeyCode.Mouse0) && !player.GetComponent<playerBehaviour>().hasReloaded)
        {
            time = nextFrame;
            EnableLaser();
            endVFX.SetActive(true);
        }

        if (startedShooting && player.GetComponent<playerBehaviour>().bulletsYellow > 0)
        {
            CheckFirstAbsorb();
            UpdateLaser();
            ScreenShake.shake = 1.5f;
            ScreenShake.canShake = true;
            cursor.GetComponent<Animator>().SetTrigger("click");
        }

        if (time >= nextFrame && startedShooting && !Input.GetKeyUp(KeyCode.Mouse0) &&
            player.GetComponent<playerBehaviour>().bulletsYellow > 0)
        {
            nextFrame += period;
            player.GetComponent<playerBehaviour>().bulletsYellow--;
            SoundManagerScript.PlaySound("yellowGun");
        }
        else if (time >= nextFrame && Input.GetKeyDown(KeyCode.Mouse0) &&
                 player.GetComponent<playerBehaviour>().bulletsYellow > 0 &&
                 this.gameObject.activeInHierarchy == true && !startedShooting &&
                 !player.GetComponent<playerBehaviour>().hasReloaded &&
                 !player.GetComponent<playerBehaviour>().weaponMenuUp)
        {
            UpdateLaser();
            nextFrame += period;
            startedShooting = true;
            player.GetComponent<playerBehaviour>().bulletsYellow--;
            SoundManagerScript.PlaySound("yellowGun");
            ScreenShake.shake = 1.5f;
            ScreenShake.canShake = true;
            CheckFirstAbsorb();
        }

        if ((Input.GetKeyUp(KeyCode.Mouse0) && startedShooting) ||
            player.GetComponent<playerBehaviour>().bulletsYellow == 0)
        {
            startedShooting = false;
            DisableLaser();
        }

        //POPUPS
        /* if (time >= nextFrame && Input.GetButton("Fire1") && player.GetComponent<playerBehaviour>().bulletsYellow == 0 &&
             this.gameObject.activeInHierarchy == true && player.GetComponent<playerBehaviour>().reservedAmmoYellow > 0 && !player.GetComponent<playerBehaviour>().isReloading && !player.GetComponent<playerBehaviour>().weaponMenuUp)
         {
             reloadText.SetActive(true);
         }*/

        if (time >= nextFrame && Input.GetButton("Fire1") &&
            player.GetComponent<playerBehaviour>().bulletsYellow == 0 &&
            this.gameObject.activeInHierarchy == true &&
            player.GetComponent<playerBehaviour>().reservedAmmoYellow == 0 &&
            !player.GetComponent<playerBehaviour>().weaponMenuUp)
        {
            noAmmoText.SetActive(true);
        }

        if (noAmmoText.activeInHierarchy)
        {
            cdAmmo += Time.deltaTime;
            if (cdAmmo >= maxCdAmmo)
            {
                noAmmoText.SetActive(false);
                cdAmmo = 0;
            }
        }

        time += Time.deltaTime;
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
            endVFX.SetActive(true);
        }
    }

    void UpdateLaser()
    {
        Vector2 mousePos = (Vector2) cam.ScreenToWorldPoint(Input.mousePosition);
        lineRenderer.SetPosition(0, (Vector2) firePoint.position);
        startVFX.transform.position = firePoint.position;
        lineRenderer.SetPosition(1, mousePos);

        Vector2 direction = mousePos - (Vector2) transform.position;

        RaycastHit2D hit = Physics2D.Raycast((Vector2) firePoint.position, direction.normalized, direction.magnitude,
            hittableMasK);

        if (hit)
        {
            if (hit.collider.CompareTag("Enemy") && time >= nextFrame)
            {
                if (hit.collider.name == "NeedNameEnemy(Clone)")
                {
                    hit.collider.GetComponent<demoEnemyBehaviour>().lifes -= bulletForce;
                    popUpDamage(bulletForce, hit);
                }
                else if (hit.collider.name == "DemoEnemy2(Clone)")
                {
                    hit.collider.GetComponent<demoEnemyBehaviour2>().lifes -= bulletForce;
                    popUpDamage(bulletForce, hit);
                }
                else if (hit.collider.name == "EnemyPatrol2(Clone)")
                {
                    hit.collider.GetComponent<EnemyPatrol2>().lifes -= bulletForce;
                    popUpDamage(bulletForce, hit);
                }
                else if (hit.collider.name == "BurstEnemy(Clone)")
                {
                    hit.collider.GetComponent<burstEnemyBehaviour>().lifes -= bulletForce;
                    popUpDamage(bulletForce, hit);
                }
                else 
                {
                    hit.collider.GetComponent<radialEnemyBehaviour>().lifes -= bulletForce;
                    popUpDamage(bulletForce, hit);
                }
            }

            if (hit.transform.tag == "Boss" && time >= nextFrame)
            {
                if (hit.transform.name == "Boss Knight")
                {
                    hit.transform.GetComponent<BossKhightBehaviour>().health -= (int) bulletForce;
                    float slider = bulletForce / hit.transform.GetComponent<BossKhightBehaviour>().maxHealth;
                    hit.transform.GetComponent<BossKhightBehaviour>().sliderHealth.transform.GetChild(2)
                        .GetComponent<Image>().fillAmount -= slider;
                    popUpDamage(bulletForce, hit);
                }
                else
                {
                    hit.collider.GetComponent<BossPhaseBehaviour>().health -= (int) bulletForce;
                    float slider = bulletForce / hit.collider.GetComponent<BossPhaseBehaviour>().maxHealth;
                    hit.collider.GetComponent<BossPhaseBehaviour>().sliderHealth.transform.GetChild(2)
                        .GetComponent<Image>().fillAmount -= slider;
                    popUpDamage(bulletForce, hit);
                }
            }

            if (hit.collider.CompareTag("BarrilesExplosivos") && time >= nextFrame)
            {
                hit.collider.GetComponent<Animator>().SetTrigger("hit");
                hit.collider.GetComponent<explosiveBarrel>().lifes--;
            }

            if (hit.collider.CompareTag("RobotPatrol") && time >= nextFrame)
            {
                hit.collider.GetComponent<enemyPatrol>().lifes -= bulletForce;
                popUpDamage(bulletForce, hit);
            }

            if (hit.collider.CompareTag("Barriles") && time >= nextFrame)
            {
                hit.collider.GetComponent<barrilScript>().lifes--;
            }

            if (hit.collider.CompareTag("Tower") && time >= nextFrame)
            {
                hit.transform.GetComponent<TowerBehaviour>().lifes -= bulletForce;
                popUpDamage(bulletForce, hit);
            }

            if (hit.transform.CompareTag("AlienEnemy") && time >= nextFrame)
            {
                hit.collider.GetComponent<AlienBehaviour>().laserDamage = true;
                popUpDamage(bulletForce, hit);
            }
            if(hit.transform.CompareTag("BubbleTrigger") && time >= nextFrame)
            {
                Debug.Log("Hit");
                if(hit.transform.GetComponent<spawnBallLeft>())
                {
                    if(hit.transform.GetComponent<spawnBallLeft>().Yellow)
                    {
                        hit.transform.GetComponent<spawnBallLeft>().doYesYellow();
                    }
                    else
                    {
                        hit.transform.GetComponent<spawnBallLeft>().doNoYellow();
                    }
                }
                else if (hit.transform.GetComponent<spawnBallRight>())
                {
                    if (hit.transform.GetComponent<spawnBallRight>().Yellow)
                    {
                        hit.transform.GetComponent<spawnBallRight>().doYesYellow();
                    }
                    else
                    {
                        hit.transform.GetComponent<spawnBallRight>().doNoYellow();
                    }
                }
                else if (hit.transform.GetComponent<spawnBallUp>())
                {
                    if (hit.transform.GetComponent<spawnBallUp>().Yellow)
                    {
                        hit.transform.GetComponent<spawnBallUp>().doYesYellow();
                    }
                    else
                    {
                        hit.transform.GetComponent<spawnBallUp>().doNoYellow();
                    }
                }
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
            endVFX.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && player.GetComponent<playerBehaviour>().bulletsYellow >= 3)
        {
            damage = 10f;
            startedShooting = true;
        }

        if (startedShooting)
        {
            canShoot -= Time.deltaTime;
            ScreenShake.shake = 0.2f;
            ScreenShake.canShake = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) && player.GetComponent<playerBehaviour>().bulletsYellow >= 3 &&
            startedShooting && canShoot <= 0)
        {
            startedShooting = false;
            player.GetComponent<playerBehaviour>().bulletsYellow -= 3;
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

        /* if (player.GetComponent<playerBehaviour>().bulletsYellow < 3)
         {
             reloadText.SetActive(true);
         }
         else
         {
             reloadText.SetActive(false);
         }*/

        if (player.GetComponent<playerBehaviour>().bulletsYellow < 3 &&
            player.GetComponent<playerBehaviour>().reservedAmmoYellow +
            player.GetComponent<playerBehaviour>().bulletsYellow < 3)
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

    void popUpDamage(float hitdamage, RaycastHit2D hit2D)
    {
        GameObject dmg = Instantiate(hitDamagePopUp,
            new Vector3(hit2D.collider.transform.position.x, hit2D.collider.transform.position.y, -0.15f),
            Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }
}