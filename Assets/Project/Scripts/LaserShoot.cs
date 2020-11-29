using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{

    private float distance;
    public LineRenderer line;
    public Transform l_transform;
    public static float damage;
    // Start is called before the first frame update
    void Start()
    {
        l_transform = GetComponent<Transform>();
        distance = 100;
        damage = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
        else
        {
            line.enabled = false;
        }
    }

    void Shoot()
    {
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
