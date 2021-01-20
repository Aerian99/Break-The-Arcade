using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvadersTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    bool nextRoom = false;
    public GameObject cam1, cam2, cam3, globalLight, lightPlayer, lightEnemies, spaceInvLimit,
        focoR, focoG, focoB;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            RenderSettings.ambientLight = Color.black;
            cam1.SetActive(false);
            cam2.SetActive(false);
            cam3.SetActive(true);
            globalLight.SetActive(true); 
            lightPlayer.SetActive(true);
            lightEnemies.SetActive(true);
            spaceInvLimit.SetActive(true);
            focoR.SetActive(false);
            focoG.SetActive(false);
            focoB.SetActive(false);

        }

    }
}
