using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class AlienBehaviour : MonoBehaviour
{
    private Animator anim;

    [HideInInspector] public GameObject[] hitDamagePopUp;
    private float actualHealth;
    private float maxHealth;
    public Image life;

    [HideInInspector] public bool laserDamage;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 15f;
        actualHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actualHealth <= 0)
        {
            anim.SetBool("dead", true);
            Destroy(this.gameObject, 0.1f);
        }

        if (laserDamage)
        {
            anim.SetTrigger("hit");
            actualHealth -= LaserShoot.damage;
            life.fillAmount -= LaserShoot.damage / maxHealth;
            popUpDamage(LaserShoot.damage);
            laserDamage = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.tag == "PurpleBullet")
            {
                anim.SetTrigger("hit");
                actualHealth -= PurpleShoot.bulletDamage;
                life.fillAmount -= PurpleShoot.bulletDamage / maxHealth;
                popUpDamage(PurpleShoot.bulletDamage);
            }
            else if (other.gameObject.tag == "YellowBullet")
            {
                anim.SetTrigger("hit");
                actualHealth -= YellowShoot.bulletDamage;
                life.fillAmount -= YellowShoot.bulletDamage / maxHealth;
                popUpDamage(YellowShoot.bulletDamage);
            }
            else if (other.gameObject.tag == "RedBullet")
            {
                anim.SetTrigger("hit");
                actualHealth -= RedShoot.bulletDamage;
                life.fillAmount -= RedShoot.bulletDamage / maxHealth;
                popUpDamage(RedShoot.bulletDamage);
            }
        
    }

    void popUpDamage(float hitdamage)
    {
        GameObject dmg = Instantiate(hitDamagePopUp[Random.Range(0, 4)], transform.position, Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }
}
