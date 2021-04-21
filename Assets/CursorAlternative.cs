using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorAlternative : MonoBehaviour
{
    public bool coinAdded;
    public Sprite normalCursor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Canvas").transform.GetChild(0).GetComponent<MainMenuManager>().pressedPlay)
        {
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        
        if (SceneManager.GetActiveScene().name == "Achievements")
        {
            coinAdded = true;
        }
        if (!coinAdded)
        {
            CursorMoneda();
        }
        else if (coinAdded)
        {
            CursorRotation();
        }

    }

    void CursorMoneda()
    {
        Cursor.visible = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    void CursorRotation()
    {
        this.GetComponent<Animator>().enabled = false;
        Cursor.visible = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
        this.GetComponent<SpriteRenderer>().sprite = normalCursor;
        transform.rotation *= Quaternion.Euler(0,0, 150f * Time.deltaTime);
        transform.localScale = new Vector3(150f, 150f, 150f);
    }
}
