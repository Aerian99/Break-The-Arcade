using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoss : MonoBehaviour
{
    public GameObject cam1, cam2, cam3, camBoss;
    public GameObject camMiniMap, roomToFocusMiniMap;
    public bool firstTime;
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
        if (collision.CompareTag("Player"))
        {
            camMiniMap.transform.position = new Vector3(roomToFocusMiniMap.transform.position.x, roomToFocusMiniMap.transform.position.y, camMiniMap.transform.position.z);
            if (firstTime)
            {
                cam1.SetActive(false);
                cam2.SetActive(false);
                cam3.SetActive(false);
                camBoss.SetActive(true);
                firstTime = false;
            }
            else
            {
                cam1.SetActive(true);
                cam2.SetActive(false);
                camBoss.SetActive(true);
                firstTime = true;
            }
        }
    }
}
