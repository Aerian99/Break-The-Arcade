using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private LayerMask hittableMasK, hittableMask2;


    private float nextFrame;
    private float time;
    private float period;
    private float canShoot, maxShoot;

    public GameObject reloadText;
    public GameObject noAmmoText;
    private float maxCdAmmo, cdAmmo;

    public GameObject hitDamagePopUp;
    [HideInInspector]public float bulletForce = 5f;
    
    public GameObject cursor;

    // Start is called before the first frame update
    void Start()
    {
        hittableMasK = LayerMask.GetMask("Enemy", "Barril", "BarrilExplosivo", "Platforms");
        //distance = 100;
        startedShooting = false;
        damage = 3f + GameObject.Find("Quest Saver").GetComponent<QuestSaver>().m_PowerUps.damageLaserGun;
        nextFrame = 0;
        time = 0;
        period = 0.1f;
        maxShoot = 1f;
        canShoot = maxShoot;
        maxCdAmmo = 1.1f;
        cdAmmo = 0.0f;
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
            if (Input.GetKeyDown(KeyCode.Mouse0) && !playerBehaviour.isReloading)
            {
                time = nextFrame;
                EnableLaser();
                endVFX.SetActive(true);
            }

            if (startedShooting && playerBehaviour.bulletsYellow > 0)
            {
                CheckFirstAbsorb();
                UpdateLaser();
                ScreenShake.shake = 1.5f;
                ScreenShake.canShake = true;
                cursor.GetComponent<Animator>().SetTrigger("click");
            }

            if (time >= nextFrame && startedShooting && !Input.GetKeyUp(KeyCode.Mouse0) &&
                playerBehaviour.bulletsYellow > 0)
            {
                nextFrame += period;
                playerBehaviour.bulletsYellow--;
                SoundManagerScript.PlaySound("yellowGun");
            }
            else if (time >= nextFrame && Input.GetKeyDown(KeyCode.Mouse0) && playerBehaviour.bulletsYellow > 0 &&
                     this.gameObject.activeInHierarchy == true && !startedShooting && !playerBehaviour.isReloading && !playerBehaviour.weaponMenuUp)
            {
                UpdateLaser();
                nextFrame += period;
                startedShooting = true;
                playerBehaviour.bulletsYellow--;
                SoundManagerScript.PlaySound("yellowGun");
                ScreenShake.shake = 1.5f;
                ScreenShake.canShake = true;
                CheckFirstAbsorb();
            }

            if ((Input.GetKeyUp(KeyCode.Mouse0) && startedShooting) || playerBehaviour.bulletsYellow == 0)
            {
                startedShooting = false;
                DisableLaser();
            }

            //POPUPS
            if (time >= nextFrame && Input.GetButton("Fire1") && playerBehaviour.bulletsYellow == 0 &&
                this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoYellow > 0 && !playerBehaviour.isReloading && !playerBehaviour.weaponMenuUp)
            {
                reloadText.SetActive(true);
            }

            else if (time >= nextFrame && Input.GetButton("Fire1") && playerBehaviour.bulletsYellow == 0 &&
                     this.gameObject.activeInHierarchy == true && playerBehaviour.reservedAmmoYellow == 0 && !playerBehaviour.weaponMenuUp)
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
        
        RaycastHit2D hit = Physics2D.Raycast((Vector2) firePoint.position, direction.normalized, direction.magnitude, hittableMasK);

        if (hit)
        {
            if (hit.collider.CompareTag("Enemy") && time >= nextFrame)
            {
                hit.collider.GetComponent<radialEnemyBehaviour>().lifes -= bulletForce;
                popUpDamage(bulletForce, hit);
            }
            if (hit.transform.tag == "Boss" && time >= nextFrame)
            {
                if (hit.transform.name == "Boss Knight")
                {
                    hit.transform.GetComponent<BossKhightBehaviour>().health -= (int)bulletForce;
                    float slider = bulletForce / hit.transform.GetComponent<BossKhightBehaviour>().maxHealth;
                    hit.transform.GetComponent<BossKhightBehaviour>().sliderHealth.transform.GetChild(2).GetComponent<Image>().fillAmount -= slider;
                    popUpDamage(bulletForce, hit);
                }
                else
                {
                    hit.collider.GetComponent<BossPhaseBehaviour>().health -= (int)bulletForce;
                    float slider = bulletForce / hit.collider.GetComponent<BossPhaseBehaviour>().maxHealth;
                    hit.collider.GetComponent<BossPhaseBehaviour>().sliderHealth.transform.GetChild(2).GetComponent<Image>().fillAmount -= slider;
                    popUpDamage(bulletForce, hit);
                }
                
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerBehaviour.bulletsYellow >= 3)
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
    void popUpDamage(float hitdamage, RaycastHit2D hit2D)
    {
        GameObject dmg = Instantiate(hitDamagePopUp, new Vector3(hit2D.collider.transform.position.x, hit2D.collider.transform.position.y, -0.15f), Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }
}