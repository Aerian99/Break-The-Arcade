using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasPassedLvl = false;
    bool nextRoom = false;
    public GameObject spaceCam, globalLight, oldGlobalLight, lightPlayer, spaceInvLimit,
        focoR, focoG, focoB, enemies;
    public GameObject camMiniMap, roomToFocusMiniMap, roomToFocusMiniMapLvl;

    private void Start()
    {
        hasPassedLvl = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if(!hasPassedLvl)
            {
                camMiniMap.transform.position = new Vector3(roomToFocusMiniMap.transform.position.x, roomToFocusMiniMap.transform.position.y, camMiniMap.transform.position.z);
                RenderSettings.ambientLight = Color.black;
                spaceCam.SetActive(true);
                oldGlobalLight.SetActive(false);
                globalLight.SetActive(true); 
                lightPlayer.SetActive(true);
                spaceInvLimit.SetActive(true);
                focoR.SetActive(false);
                focoG.SetActive(false);
                focoB.SetActive(false);
                enemies.SetActive(true);
            }
            else
            {
                camMiniMap.transform.position = new Vector3(roomToFocusMiniMapLvl.transform.position.x, roomToFocusMiniMapLvl.transform.position.y, camMiniMap.transform.position.z);
                spaceCam.SetActive(false);
                oldGlobalLight.SetActive(true);
                globalLight.SetActive(false);
                lightPlayer.SetActive(false);
                spaceInvLimit.SetActive(false);
                focoR.SetActive(true);
                focoG.SetActive(true);
                focoB.SetActive(true);
                enemies.SetActive(false);
            }

        }

    }
}
