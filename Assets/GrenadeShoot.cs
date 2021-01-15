using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GrenadeShoot : MonoBehaviour
{
    public int numBullets;
    private Vector3 startPoint;
    private const float radius = 1f;
    public float bulletSpeed;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        startPoint = transform.position;
        startPoint = new Vector3(startPoint.x, startPoint.y + .5f, startPoint.z);
        float angleStep = 360f / numBullets;
        float angle = 0f;

        
            for (int i = 0; i <= numBullets - 1; i++)
            {
                float bulletDirXPosition = startPoint.x + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
                float bulletDirYPosition = startPoint.y + Mathf.Sin((angle * Mathf.PI) / 180) * radius;

                Vector3 bulletVector = new Vector3(bulletDirXPosition, bulletDirYPosition, 0);
                Vector3 bulletMoveDirection = (bulletVector - startPoint).normalized * bulletSpeed;

                GameObject bulletGO = Instantiate(bulletPrefab, startPoint, quaternion.identity);
                bulletGO.transform.rotation = UnityEngine.Quaternion.AngleAxis(angle, Vector3.forward);
                bulletGO.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletMoveDirection.x, bulletMoveDirection.y, 0);
                angle += angleStep;
            }
    }
}
