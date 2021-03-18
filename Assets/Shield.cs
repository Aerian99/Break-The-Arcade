using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject popUp;
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
        if(collision.tag == "EnemyBullet" || collision.tag == "Bullet Pacman")
        {
            collision.tag = "PurpleBullet";
            Vector3 eulerAngle = new Vector3(collision.GetComponent<Transform>().rotation.eulerAngles.x, collision.GetComponent<Transform>().rotation.eulerAngles.y, collision.GetComponent<Transform>().rotation.eulerAngles.z);
            if(collision.GetComponent<Transform>().rotation.eulerAngles.z + 180f > 360)
            {
                collision.GetComponent<Transform>().transform.Rotate(new Vector3(0,0,180));
            }
            collision.gameObject.layer = LayerMask.NameToLayer("Bullet");
            collision.gameObject.AddComponent<purpleBulletBehaviour>();
            collision.gameObject.GetComponent<purpleBulletBehaviour>().hitEffectPrefab = particle;
            collision.gameObject.GetComponent<purpleBulletBehaviour>().hitDamagePopUp = popUp;
            collision.GetComponent<Rigidbody2D>().velocity *= -1;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "EnemyBullet" || collision.transform.tag == "Bullet Pacman")
        {
            collision.transform.tag = "PurpleBullet";
            Vector3 eulerAngle = new Vector3(collision.transform.GetComponent<Transform>().rotation.eulerAngles.x, collision.transform.GetComponent<Transform>().rotation.eulerAngles.y, collision.transform.GetComponent<Transform>().rotation.eulerAngles.z);
            if (collision.transform.GetComponent<Transform>().rotation.eulerAngles.z + 180f > 360)
            {
                collision.transform.GetComponent<Transform>().transform.Rotate(new Vector3(0, 0, 180));
            }
            collision.gameObject.layer = LayerMask.NameToLayer("Bullet");
            collision.gameObject.AddComponent<purpleBulletBehaviour>();
            collision.gameObject.GetComponent<purpleBulletBehaviour>().hitEffectPrefab = particle;
            collision.gameObject.GetComponent<purpleBulletBehaviour>().hitDamagePopUp = popUp;
            collision.transform.GetComponent<Rigidbody2D>().velocity *= -1;

        }
    }
}
