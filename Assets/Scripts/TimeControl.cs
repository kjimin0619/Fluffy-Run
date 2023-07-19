using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeControl : MonoBehaviour
{

    public float gameTime = 30f; 
    public float time; // 잔여 시간

    public Image fillImage;
    public static TimeControl instance;

    private void Awake()
    {
        if(TimeControl.instance == null)
        {
            TimeControl.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        time = gameTime;

        GameObject timerObject = GameObject.Find("Timer");
        if (timerObject != null)
        {
            fillImage = timerObject.GetComponent<Image>();
        }
        else
        {
            Debug.LogError("Image with 'Timer' name not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateFillAmount();

        if (time <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void UpdateFillAmount()
    {
        float fillAmount = time / gameTime;
        fillImage.fillAmount = fillAmount;
    }

    public void DecreaseTime()
    {
       if (fillImage != null)
        {
            time  = time * (7f/8f);
            UpdateFillAmount();
        }
    }


}
