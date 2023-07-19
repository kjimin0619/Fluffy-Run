using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{
    public int coinCount;
    public TextMeshProUGUI coinDisplayText;
    
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
