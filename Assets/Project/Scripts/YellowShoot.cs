using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowShoot : MonoBehaviour
{
    private GameObject particlePoint;
    private Transform shootPoint;
    public GameObject bulletPrefab;
    private GameObject bullet;
    public GameObject alternativeMuzzle;
    public GameObject bulletReloadPrefab;
    private GameObject bulletReload;
    //private float distance;
    public LineRenderer line;
    public Transform l_transform;

    // BULLET SETTINGS
    public static float bulletDamage;
    /*private float bulletForce = 25f;
    private float bulletLifeTime = 0.35f;
    private float timeBetweenShots = 0.20f;*/
    private float timestamp;
    void Start()
    {
        particlePoint = this.gameObject.transform.GetChild(0).gameObject;
        shootPoint = this.gameObject.transform.GetChild(1).gameObject.transform;
        bulletDamage = 1f;
        //distance = 100;
        l_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
            SoundManagerScript.PlaySound("yellowGun");
            ScreenShake.shake = 1f;
            ScreenShake.canShake = true;
        }
    }
    void Shoot()
    {
        Debug.Log(l_transform.position);
        if (Physics2D.Raycast(l_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(l_transform.position, transform.right);
            DrawRay(l_transform.position, _hit.point);

        }
        //ReloadBullet();
    }
    void ReloadBullet()
    {
        bulletReload = Instantiate(bulletReloadPrefab, this.transform.position, Quaternion.identity);
        if (playerAimWeapon.isFacingLeft)
        {
            bulletReload.GetComponent<Rigidbody2D>().AddForce(new Vector2(6f, 7f), ForceMode2D.Impulse);
        }
        else
        {
            bulletReload.GetComponent<Rigidbody2D>().AddForce(new Vector2(-6f, 7f), ForceMode2D.Impulse);
        }
        Destroy(bulletReload, 1f);
    }

    void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    
    }
}