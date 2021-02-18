using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    public Color hoverColor;
    public Color baseColor;
    public Image background;
    
    void Start()
    {
        background.color = baseColor;
    }

    // Update is called once per frame
    public void Select()
    {
        background.color = hoverColor;
        SoundManagerScript.PlaySound("menuPick");
    }
    
    public void Deselect()
    {
        background.color = baseColor;
    }
}
