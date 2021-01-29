using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public bool isMenu, coinAdded;
    public Texture2D cursorTextureMoneda, cursorTexturePointer;
    Vector2 cursorHotspot;
    void Start()
    {
        if (isMenu)
        {
            cursorHotspot = new Vector2(cursorTextureMoneda.width / 2, cursorTextureMoneda.height / 2);
            Cursor.SetCursor(cursorTextureMoneda, cursorHotspot, CursorMode.ForceSoftware);
            Cursor.visible = true;        
        }
        else
            Cursor.visible = false;
    }

    private void Update()
    {
        if (coinAdded) 
        {
            cursorHotspot = new Vector2(cursorTexturePointer.width / 2, cursorTexturePointer.height / 2);
            Cursor.SetCursor(cursorTexturePointer, cursorHotspot, CursorMode.ForceSoftware);
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
        
        transform.rotation *= Quaternion.Euler(0,0, 50f * Time.deltaTime);
    }
}
