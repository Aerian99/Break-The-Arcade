using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    float cd, cdDeath, cdAnim;
    public bool isDead;

    private void Start()
    {
        cd = 0;
        cdAnim = 0.2f;
        cdDeath = 1.5f;
    }

    private void Update()
    {
        if (isDead)
        { 
            cd += Time.deltaTime;
            if (cd >= cdAnim)
                anim.SetBool("fadeIn", true);
            if (cd >= cdDeath)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

}

