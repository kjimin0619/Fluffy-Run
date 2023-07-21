using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class ClearCheck : MonoBehaviour
{
    [Header("현재 스테이지 번호")] public int stageNum;
    [Header("두 번째 별 획득 조건: 동전 먹기")] public int greatCoinCount;
    [Header("세 번째 별 획득 조건: 시간 남기기")] public float greatLeftTime;

    // 클리어 시 표시되는 UI
    public GameObject FinishUI;

    void Start()
    {
        PlayerPrefs.SetInt("currentPlayingStage", stageNum);
        FinishUI = GameObject.Find("FinishUI");
        FinishUI.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Character")
        {
            
            Debug.Log("Clear");
            SetClear();
            
            this.gameObject.SetActive(false);
        }
    }

    private void SetClear()
    {
        // 일시정지 처리, 클리어 UI 표시
        Time.timeScale = 0;
        FinishUI.SetActive(true);
        
        int starCount = 1;
        int coinCount = CoinCounter.Instance.coinCount;
        float leftTime = TimeControl.Instance.leftTime;

        if (coinCount >= greatCoinCount) starCount++;
        if (leftTime >= greatLeftTime) starCount++;

        int beforeStarCount = PlayerPrefs.GetInt($"Stage{stageNum}", 0);
        float beforeLeftTime = PlayerPrefs.GetFloat($"Stage{stageNum}LeftTime", 0f);
        
        Debug.Log($"Stage {stageNum} Clear! 이번에 획득한 별은 {starCount}개");

        // 최고 별 경신
        // 현재 저장된 별보다 모은 별이 더 많다면
        if (beforeStarCount < starCount)
        {
            PlayerPrefs.SetInt($"Stage{stageNum}", starCount);
            Debug.Log($"기록 경신 저장 완료. Stage {stageNum}: 별 {starCount}개");
            Debug.Log($"(경신 직전 {beforeStarCount}개)");
        }
        
        // 최고 기록 경신
        // 현재 저장된 시간보다 남은 시간이 더 많다면
        if (beforeLeftTime < leftTime)
        {
            PlayerPrefs.SetFloat($"Stage{stageNum}LeftTime", leftTime);
            Debug.Log($"기록 경신 저장 완료. Stage {stageNum}: 남은 시간 {leftTime:F2}초");
            Debug.Log($"(경신 직전 {beforeLeftTime:F2}초)");
        }
    }
}
