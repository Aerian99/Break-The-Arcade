using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class UISettings : MonoBehaviour
{
    private Image shiftSR;
    private Image spaceSR;
    public Sprite notHoldShift;
    public Sprite HoldShift;
    public Sprite notHoldSpace;
    public Sprite HoldSpace;
    public Image dashImage;
    
    void Start()
    {
        shiftSR = gameObject.transform.GetChild(8).GetComponent<Image>();
        spaceSR = gameObject.transform.GetChild(10).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        keysInfo();
    }
    

    void keysInfo()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            shiftSR.sprite = HoldShift;
        }
        else
        {
            shiftSR.sprite = notHoldShift;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            spaceSR.sprite = HoldSpace;
        }
        else
        {
            spaceSR.sprite = notHoldSpace;
        }
    }
}
