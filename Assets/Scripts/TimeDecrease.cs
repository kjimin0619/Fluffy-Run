using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TimeDecrease : MonoBehaviour
{
    [Header("닿았을 때 줄어드는 시간")]
    public float decreasing = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Character")
        {
            TimeControl.Instance.DecreaseTime(decreasing);
            this.gameObject.SetActive(false);
        }
    }
}
