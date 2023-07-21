using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMP_UI = TMPro.TextMeshProUGUI;

public class CoinCounter : MonoBehaviour
{
    public int coinCount;
    public TMP_UI coinDisplayText;
    
    private static CoinCounter instance = null;
    public static CoinCounter Instance => instance;

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
        coinCount = 0;
    }

    public void AddCoin()
    {
        coinCount++;
        coinDisplayText.text = coinCount.ToString();
    }

    void SetCoin(int val)
    {
        coinCount = val;
        coinDisplayText.text = coinCount.ToString();
    }
}
