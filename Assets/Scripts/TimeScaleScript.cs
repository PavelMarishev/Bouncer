using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeScaleScript : MonoBehaviour
{
    public Text gameSpeed;
    public void faster()
    {
        Time.timeScale *= 2;
        gameSpeed.text = "x" + Time.timeScale.ToString();
    }
    public void slower()
    {
        if (Time.timeScale > 1)
        {
            Time.timeScale /= 2;
            gameSpeed.text = "x" + Time.timeScale.ToString();
        }
    }

    public void setDefault()
    {
        Time.timeScale = 1;
        gameSpeed.text = "x" + Time.timeScale.ToString();
    }
}
