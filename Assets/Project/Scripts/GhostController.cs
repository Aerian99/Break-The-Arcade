using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public GameObject ghostPrefab;
    private float delay;
    float delta = 0;

    playerMovement player;
    SpriteRenderer spriteRenderer;
    private float destroTime;
    private Color color;
    private Material material = null;
    void Start()
    {
        player = GetComponent<playerMovement>();
        delay = 0.02f;
        destroTime = 0.1f;
        color = new Color(1, 1, 1, 0.2470588f);
    }

    // Update is called once per frame
    void Update()
    {
        if (delta > 0)
        {
            delta -= Time.deltaTime;
        }
        else
        {
            delta = delay;
            createGhost();
        }
    }
    void createGhost() 
    {
        GameObject ghostObj = Instantiate(ghostPrefab, transform.position, transform.rotation);
        ghostObj.transform.localScale = player.transform.localScale;
        Destroy(ghostObj, destroTime);

        spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = player.p_sprite.sprite;
        spriteRenderer.color = color;

        if (playerAimWeapon.isFacingLeft == true) 
        {
            spriteRenderer.flipX = true;
        } else 
        {
            spriteRenderer.flipX = false;
        }

        if (material != null) 
        {
            spriteRenderer.material = material;
        }
    }
}
