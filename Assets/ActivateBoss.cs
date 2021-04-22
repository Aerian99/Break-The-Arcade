using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    public GameObject BossObject;
    public GameObject[] doors;

    private void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossObject.SetActive(true);
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<Animator>().SetBool("hasPassed", true);
        }
    }
}
