using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAmmo : MonoBehaviour
{
    public bool activeBoss = false;
    public GameObject ammoPrefab, GO;
    public int position, positionY;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(throwAmmoType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator throwAmmoType()
    {
        while (true)
        {
            if(activeBoss)
            {
                GO = Instantiate(ammoPrefab, transform.position, Quaternion.identity);
                Vector2 vec = new Vector2(position, positionY) * Time.deltaTime;
                GO.GetComponent<Rigidbody2D>().AddForce(vec);
                yield return new WaitForSeconds(30f);
            }



            yield return new WaitForSeconds(0);

        }




    }
}
