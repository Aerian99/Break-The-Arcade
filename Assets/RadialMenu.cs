using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    public Vector2 normalisedMousePosition;
    public float currentAngle;
    public static int selection;
    private int previousSelection;

    public GameObject[] menuItems;
    private MenuItemScript menuItemSc;
    private MenuItemScript previousMenuItemSc;

    public GameObject name1, name2, name3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        normalisedMousePosition = new Vector2(
            Input.mousePosition.x - Screen.width / 2, 
            Input.mousePosition.y - Screen.height / 2);
        currentAngle = Mathf.Atan2(normalisedMousePosition.y, normalisedMousePosition.x) * Mathf.Rad2Deg;

        currentAngle = (currentAngle + 360f) % 360f;
        selection = (int) currentAngle / 120;

        if (selection != previousSelection)
        {
            previousMenuItemSc = menuItems[previousSelection].GetComponent<MenuItemScript>();
            previousMenuItemSc.Deselect();
            previousSelection = selection;

            menuItemSc = menuItems[selection].GetComponent<MenuItemScript>();
            menuItemSc.Select();
        }
        Debug.Log(selection);

        if (selection == 0)
        {
            name1.SetActive(true);
            name2.SetActive(false);
            name3.SetActive(false);
        }
        else if (selection == 1)
        {
            name1.SetActive(false);
            name2.SetActive(true);
            name3.SetActive(false);
        }
        else if (selection == 2)
        {
            name1.SetActive(false);
            name2.SetActive(false);
            name3.SetActive(true);
        }
    }
}
