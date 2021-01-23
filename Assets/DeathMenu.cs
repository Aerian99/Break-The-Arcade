using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    void Start()
    {
        StartCoroutine(GoMenu());
    }

    IEnumerator GoMenu()
    {
        
        yield return new WaitForSeconds(3);
        anim.SetBool("fadeIn", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("MainMenu");

    }
}
