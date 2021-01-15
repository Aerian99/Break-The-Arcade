using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDController : MonoBehaviour
{
    float timeLeft;
    Color targetColor;
    public Material material;
    float intensity;
    public GameObject TilemapBordes;

    void Update()
    {
        if (ScreenShake.canShake)
        {
            intensity = 0f;
            targetColor = new Color(Random.value * intensity, Random.value * intensity, Random.value * intensity);
            material.color = targetColor;
        }
        else 
        {
            intensity = Mathf.Pow(2, 1.2f);
        
            if (timeLeft <= Time.deltaTime)
            {
                material.color = targetColor;

                targetColor = new Color(Random.value * intensity, Random.value * intensity, Random.value * intensity);
                timeLeft = 2.0f;
            }
            else
            {

                material.color = Color.Lerp(material.color, targetColor, Time.deltaTime / timeLeft);
                timeLeft -= Time.deltaTime;
            }
        }

    }
}
