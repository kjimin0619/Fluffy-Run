using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider timerSlider;
    public Text timerText;
    public float gameTime;

    private bool stopTimer;

    private void Awake()
    {
    }

    public void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
    }

    public void Update()
    {
        float time = gameTime - Time.time;
        if (time <= 0)
        {
            stopTimer = true;
            SceneManager.LoadScene("GameOverScene");
        }

        if (stopTimer == false)
        {
            timerSlider.value = time;
        }
    }

}
