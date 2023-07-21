using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMP_UI = TMPro.TextMeshProUGUI;

public class TimeControl : MonoBehaviour
{

    public float gameTime = 30f; 
    public float leftTime;
    public Image healthBar;
    public TMP_UI leftTimeText;

    private static TimeControl instance = null;
    public static TimeControl Instance => instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void Start()
    {
        leftTime = gameTime;
        healthBar = this.transform.Find("FrontBar").gameObject.GetComponent<Image>();
    }
    
    void Update()
    {
        leftTime -= Time.deltaTime;

        if (leftTime <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        
        UpdateLeftAmount();
    }

    private void UpdateLeftAmount()
    {
        float ratio = leftTime / gameTime;

        healthBar.fillAmount = ratio;
        healthBar.color = (int) (ratio * 3) switch
        {
            >= 2 => Color.green,
            1 => Color.yellow,
            _ => Color.red
        };

        leftTimeText.text = $"{leftTime:F2}";
    }

    public void DecreaseTime(float time)
    {
        leftTime -= time;
        
        if (leftTime <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
        
        UpdateLeftAmount();
    }


}
