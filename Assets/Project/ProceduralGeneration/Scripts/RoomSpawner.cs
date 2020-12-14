using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 --> bottom door
    //2 --> top door
    //3 --> left door
    //4 --> right door


    private RoomTemplates templates;
    int roomNum, roomSelection;
    public bool spawned;

    public float waitTime;

    private void Start()
    {
        waitTime = 4f;
        Destroy(gameObject, waitTime);
        spawned = false;
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn",0.3f);
    }
   

    void Spawn()
    {   if(!spawned)
        { 
            if (openingDirection == 1)
            {
                    roomNum = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[roomNum], transform.position, templates.bottomRooms[roomNum].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                    roomNum = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[roomNum], transform.position, templates.topRooms[roomNum].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                roomNum = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[roomNum], transform.position, templates.leftRooms[roomNum].transform.rotation);

            }
            else if (openingDirection == 4)
            {
                roomNum = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[roomNum], transform.position, templates.rightRooms[roomNum].transform.rotation);

            }
            spawned = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SpawnPoint"))
        {
            if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                Vector3 temp = new Vector3(transform.position.x + 5, transform.position.y + 5, transform.position.z);
                Instantiate(templates.closedRooms, temp, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}
