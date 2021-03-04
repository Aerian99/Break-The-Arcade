using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public bool alreadyCalculating;
    bool startDissapear = true;
    bool reloadingTime = false;
    float cd, maxCd, cdReloading, maxCdReloading;
    private GameObject[] _gameObjectsActual;
    // Start is called before the first frame update
    void Start()
    {
        maxCdReloading = 2f;
        cdReloading = maxCdReloading;
        maxCd = 3f;
        cd = maxCd;
        alreadyCalculating = false;
    }

    private void Update()
    {
        if(alreadyCalculating)
        { 
            if (startDissapear)
            {
                alreadyCalculating = true;
                cd -= Time.deltaTime;
                for (int y = 0; y < _gameObjectsActual.Length; y++)
                {
                    for (int i = 0; i < _gameObjectsActual[y].transform.childCount; i++)
                    {
                        _gameObjectsActual[y].transform.GetChild(i).GetComponent<Animator>().SetBool("RedPlatform", true);
                        _gameObjectsActual[y].transform.GetChild(i).GetComponent<Animator>().SetBool("PlayFade", true);
                        _gameObjectsActual[y].transform.GetChild(i).GetComponent<Animator>().SetBool("IsOver", false);
                    }
                }


                if (cd <= 0)
                {
                    for (int y = 0; y < _gameObjectsActual.Length; y++)
                    {
                        for (int i = 0; i < _gameObjectsActual[y].transform.childCount; i++)
                        {
                            _gameObjectsActual[y].transform.GetChild(i).GetComponent<Animator>().SetBool("PlayFade", false);
                        }
                    }
                    cd = maxCd;
                    startDissapear = false;
                    for (int y = 0; y < _gameObjectsActual.Length; y++)
                    {
                        _gameObjectsActual[y].gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                    }
                    reloadingTime = true;
                }
            }
            if (reloadingTime)
            {
                cdReloading -= Time.deltaTime;
                if (reloadingTime)
                {
                    if (cdReloading <= 0)
                    {
                        cdReloading = maxCdReloading;
                        reloadingTime = false;
                        for (int y = 0; y < _gameObjectsActual.Length; y++)
                        {
                            _gameObjectsActual[y].gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                            for (int i = 0; i < _gameObjectsActual[y].transform.childCount; i++)
                            {
                                _gameObjectsActual[y].transform.GetChild(i).GetComponent<Animator>().SetBool("IsOver", true);
                            }
                        }
                        startDissapear = true;
                        alreadyCalculating = false;
                    }
                }

            }
        }
    }


    public void controlPlatform(GameObject[] _platforms)
    {
        _gameObjectsActual = _platforms;
        alreadyCalculating = true;
           
    }
}
