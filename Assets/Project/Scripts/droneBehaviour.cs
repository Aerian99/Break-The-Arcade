using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class droneBehaviour : MonoBehaviour
{
    private Transform playerCharacter;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public GameObject hitDamagePopUp;
    private float actualHealth;
    private float maxHealth;
    public Image life;

    public void Awake()
    {
        playerCharacter = GameObject.FindWithTag("Player").transform;
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        maxHealth = 15f;
        actualHealth = maxHealth;
        life.fillAmount = actualHealth;
    }

    public void Update()
    {
        this.spriteRenderer.flipX = playerCharacter.transform.position.x > this.transform.position.x;
        if (actualHealth <= 0)
        {
            anim.SetBool("dead", true);
            Destroy(this.gameObject, 0.1f);
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
        GameObject dmg = Instantiate(hitDamagePopUp, transform.position, Quaternion.identity);
        dmg.GetComponent<TextMeshPro>().text = "-" + hitdamage;
    }
}