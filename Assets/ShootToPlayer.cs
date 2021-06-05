using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToPlayer : MonoBehaviour
{
    public GameObject bulletToShoot;
    private GameObject player;
    public GameObject[] firepoints;
    private float bulletSpeed;
    private int shootTimes;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletSpeed = 4f;
        shootTimes = 2;
        StartCoroutine(Shoot(shootTimes));
    }

    IEnumerator Shoot(int interactions)
    {

        Vector3 directionToShoot;
        while(gameObject.GetComponent<radialEnemyBehaviour>().lifes >= 0)
        {
            if (player)
            {
                for (int i = 0; i < firepoints.Length; i++)
                    {
                        directionToShoot = (player.transform.position - gameObject.transform.position).normalized;
                        ShootDirection(directionToShoot, firepoints[i]);
                        yield return new WaitForSeconds(0.3f);
                    }
            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    void ShootDirection(Vector3 shootDirection, GameObject firepoint)
    {
        GameObject bullet = Instantiate(bulletToShoot, firepoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
    }
}
