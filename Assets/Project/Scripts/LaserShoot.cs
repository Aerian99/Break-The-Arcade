using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{

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

    // Update is called once per frame
    void Update()
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

    void Shoot()
    {
        if (Absorb_Gun.firstTimeAbsorb1)
        {
            Absorb_Gun.firstTimeAbsorb1 = false;
            Absorb_Gun.ammoFull1 = true;
        }

        line.enabled = true;

        if (Physics2D.Raycast(l_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(l_transform.position, transform.right);
            if (_hit.collider.tag == "Enemy")
            {
                droneBehaviour.Laserdamaged = true;
            }
            else
            {
                droneBehaviour.Laserdamaged = false;
            }
            DrawRay(l_transform.position, _hit.point);
        }
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);

    }
}
