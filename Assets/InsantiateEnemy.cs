using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InsantiateEnemy : MonoBehaviour
{
    public GameObject gb;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateEnemy()
    {
        Instantiate(gb, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
