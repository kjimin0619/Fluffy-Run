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
        gameTime = 10; // 10초로 설정
        GameObject[] sliderObjects = GameObject.FindGameObjectsWithTag("Timer");
        if (sliderObjects.Length > 0)
        {
            timerSlider = sliderObjects[0].GetComponent<Slider>();
            stopTimer = false;
            timerSlider.maxValue = gameTime;
            timerSlider.value = gameTime;
        }
        else
        {
            Debug.LogError("Slider with 'Timer' tag not found.");
        }
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
