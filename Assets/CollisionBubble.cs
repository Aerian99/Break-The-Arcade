using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionBubble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "BubbleTrigger")
        {
            GameObject.Find("GameController").GetComponent<GameController>().playerCaracteristics.lifes = 0;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            GameObject.Find("-----SCENEMANAGEMENT").GetComponent<PlaySceneManager>().isDead = true;

        }
    }
}
