using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsWin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 velocity = new Vector2(Random.Range(-3, 3), 7);
        float angle;
        angle = 20 * Mathf.Deg2Rad;

        //z  uniformly on [cos0,1]
        float Z = RandomFloat(Mathf.Cos(angle), 1);

        //fi  uniformly on [0,2pi).
        float fi = RandomFloat(0, (2 * 3.14f));

        //to obtain the vector(sqrt 1-z2 cos angle, sqrt 1-z2 sin fi, z)
        float pointX = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Cos(fi);
        float pointY = Mathf.Sqrt(1 - Mathf.Pow(Z, 2)) * Mathf.Sin(fi);
        float pointZ = Z;

        Vector3 normalizedVector = Vector3.Normalize(new Vector3(pointX, pointY, pointZ));
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(normalizedVector.x += velocity.x, normalizedVector.y += velocity.y, 0), ForceMode2D.Impulse);
    }

    public float RandomFloat(float min, float max)
    {
        return Random.Range(min, max);
    }

}
