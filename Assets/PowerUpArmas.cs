using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpArmas : MonoBehaviour
{
    public List<Sprite> armas;
    int randomArma;
    Interpolator _interpolator = new Interpolator(1f, Interpolator.Type.SMOOTH);
    Vector3 _position;
    public float interpolationValue;
    public bool isUnlocked;

    public GameObject defaultGun, laserGun, shootGun;
    [HideInInspector]public GameObject defaultGunBullet, shootGunBullet;
    [HideInInspector]public Sprite defaultBulletStandard, defaultBulletGreen, defaultBulletBlue, shootGunBulletStandard,shootGunBulletGreen, shootGunBulletBlue;
    [HideInInspector]public Material defaultMatStandard, defaultMatGreenBullet, defaultMatBlueBullet, shootGunMatStandard,shootGunMatGreenBullet, shootGunMatBlueBullet;
    [HideInInspector]public ParticleSystem defaultStandardHit, defaultGreenHit, defaultBlueHit, shootGunStandardHit, shootGunGreenHit, shootGunBlueHit;


    // Start is called before the first frame update
    void Start()
    {
        
        _interpolator.ToMax();
        randomArma = Random.Range(0, 6);

        while(isUnlocked && randomArma == 3 || isUnlocked && randomArma == 2)
        {
            randomArma = Random.Range(0, 6);
        }
        if (randomArma == 4)
            GetComponent<SpriteRenderer>().sprite = armas[6];
        else if (randomArma == 5)
            GetComponent<SpriteRenderer>().sprite = armas[7];
        else
            GetComponent<SpriteRenderer>().sprite = armas[randomArma];

        defaultGunBullet.GetComponent<SpriteRenderer>().sprite = defaultBulletStandard;
        defaultGunBullet.GetComponent<SpriteRenderer>().material = defaultMatStandard;
        defaultGunBullet.GetComponent<purpleBulletBehaviour>().hitEffectPrefab = defaultStandardHit;

        shootGunBullet.GetComponent<SpriteRenderer>().sprite = shootGunBulletStandard;
        shootGunBullet.GetComponent<SpriteRenderer>().material = shootGunMatStandard;
        shootGunBullet.GetComponent<redBulletBehaviour>().hitEffectPrefab = shootGunStandardHit;
    }

    // Update is called once per frame
    void Update()
    {
        _interpolator.Update(Time.deltaTime);

        if (_interpolator.IsMaxPrecise)
            _interpolator.ToMin();
        else if (_interpolator.IsMinPrecise)
            _interpolator.ToMax();
        _position.y = 1 * 1 * _interpolator.Value + interpolationValue;
        _position.x = this.transform.position.x;
        this.transform.position = _position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManagerScript.PlaySound("powerup");
            switch (randomArma)
            {
                //Basic Weapon Green
                case 0:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0)
                        .GetComponent<PurpleShoot>().greenPowerUp = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0)
                        .GetComponent<PurpleShoot>().bluePowerUp = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0)
                        .GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0)
                        .GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
                    defaultGun.GetComponent<Image>().sprite = armas[randomArma];
                    defaultGunBullet.GetComponent<SpriteRenderer>().sprite = defaultBulletGreen;
                    defaultGunBullet.GetComponent<SpriteRenderer>().material = defaultMatGreenBullet;
                    defaultGunBullet.GetComponent<purpleBulletBehaviour>().hitEffectPrefab = defaultGreenHit;
                    break;

                //Basic Weapon Blue
                case 1:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0)
                        .GetComponent<PurpleShoot>().bluePowerUp = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0)
                        .GetComponent<PurpleShoot>().greenPowerUp = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0)
                        .GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0)
                        .GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
                    defaultGun.GetComponent<Image>().sprite = armas[randomArma];
                    defaultGunBullet.GetComponent<SpriteRenderer>().sprite = defaultBulletBlue;
                    defaultGunBullet.GetComponent<SpriteRenderer>().material = defaultMatBlueBullet;
                    defaultGunBullet.GetComponent<purpleBulletBehaviour>().hitEffectPrefab = defaultBlueHit;
                    break;

                //Laser Weapon Green
                case 2:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2)
                        .GetComponent<LaserShoot>().bulletForce = 7;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(0).GetComponent<LineRenderer>().materials[0]
                        .SetColor("Color_", new Color(0, 1 * 4.5f, 0));
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2)
                        .GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    // START VFX
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(1).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(1).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
                    // END VFX
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(2).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);

                    laserGun.GetComponent<Image>().sprite = armas[randomArma];
                    break;

                //Laser Weapon Blue
                case 3:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2)
                        .GetComponent<LaserShoot>().bulletForce = 11;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(0).GetComponent<LineRenderer>().materials[0]
                        .SetColor("Color_", new Color(0, 0, 1 * 4.5f));
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2)
                        .GetComponent<SpriteRenderer>().sprite = armas[randomArma];

                    // START VFX
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(1).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(1).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
                    // END VFX
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(2).GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1)
                        .GetChild(2).GetChild(1).GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);

                    laserGun.GetComponent<Image>().sprite = armas[randomArma];
                    break;

                //Shotgun Weapon Green
                case 4:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3)
                        .GetComponent<RedShoot>().powerUpGreen = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3)
                        .GetComponent<RedShoot>().powerUpBlue = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3)
                        .GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetChild(0)
                        .GetComponent<ParticleSystem>().startColor = new Color(0, 1 * 4.5f, 0);
                    shootGun.GetComponent<Image>().sprite = armas[randomArma];
                    shootGunBullet.GetComponent<SpriteRenderer>().sprite = shootGunBulletGreen;
                    shootGunBullet.GetComponent<SpriteRenderer>().material = shootGunMatGreenBullet;
                    shootGunBullet.GetComponent<redBulletBehaviour>().hitEffectPrefab = shootGunGreenHit;
                    break;

                //Shotgun Weapon Blue
                case 5:
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3)
                        .GetComponent<RedShoot>().powerUpBlue = true;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3)
                        .GetComponent<RedShoot>().powerUpGreen = false;
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3)
                        .GetComponent<SpriteRenderer>().sprite = armas[randomArma];
                    GameObject.FindGameObjectWithTag("Player").transform.GetChild(4).GetChild(0).GetChild(3).GetChild(0)
                        .GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1 * 4.5f);
                    shootGun.GetComponent<Image>().sprite = armas[randomArma];
                    shootGunBullet.GetComponent<SpriteRenderer>().sprite = shootGunBulletBlue;
                    shootGunBullet.GetComponent<SpriteRenderer>().material = shootGunMatBlueBullet;
                    shootGunBullet.GetComponent<redBulletBehaviour>().hitEffectPrefab = shootGunBlueHit;
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}