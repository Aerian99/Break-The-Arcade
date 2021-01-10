using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
        
        transform.rotation *= Quaternion.Euler(0,0, 50f * Time.deltaTime);
    }
}
