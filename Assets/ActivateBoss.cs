using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss : MonoBehaviour
{
    public GameObject BossObject;
    public GameObject[] doors;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossObject.SetActive(true);
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(true);
        }
    }
}
