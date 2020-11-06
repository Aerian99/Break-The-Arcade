using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeBehaviour : MonoBehaviour
{
    private Animator animator;
    private int lifes;

    private void Start() 
    {
        lifes = 5;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (lifes <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            animator.SetTrigger("hit");
            lifes--;
        }
    }
}
