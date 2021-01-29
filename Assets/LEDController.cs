using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDController : MonoBehaviour
{
    float timeLeft;
    Color targetColor;
    public Material material;
    float intensity;
    public static bool isTreasureRoom = false;
    bool justExitTreasure = false;
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
            if (isTreasureRoom)
            {
                intensity = Mathf.Pow(2, 1.5f);
                if (timeLeft <= Time.deltaTime)
                {
                    material.color = targetColor;
                    targetColor = new Color(1 * intensity, 0.92f * intensity, 0.016f * intensity);
                    timeLeft = 2.0f;
                    justExitTreasure = true;
                }
                else
                {
                    material.color = Color.Lerp(material.color, targetColor, Time.deltaTime / timeLeft);
                    timeLeft -= Time.deltaTime;
                }
            }
            else
            {
                if (justExitTreasure)
                {
                    timeLeft = Time.deltaTime;
                    justExitTreasure = false;
                }

                intensity = Mathf.Pow(2, 1.0f);

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
}
