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
            Vector3 eulerAngle = new Vector3(collision.GetComponent<Transform>().rotation.eulerAngles.x, collision.GetComponent<Transform>().rotation.eulerAngles.y, collision.GetComponent<Transform>().rotation.eulerAngles.z);
            if(collision.GetComponent<Transform>().rotation.eulerAngles.z + 180f > 360)
            {
                collision.GetComponent<Transform>().transform.Rotate(new Vector3(0,0,180));
            }
            collision.tag = "PurpleBullet";
            collision.gameObject.layer = LayerMask.NameToLayer("Bullet");
            collision.gameObject.AddComponent<purpleBulletBehaviour>();
            collision.gameObject.GetComponent<purpleBulletBehaviour>().hitEffectPrefab = particle;
            collision.gameObject.GetComponent<purpleBulletBehaviour>().hitDamagePopUp = popUp;
            collision.GetComponent<Rigidbody2D>().velocity *= -1;

        }
    }
}
