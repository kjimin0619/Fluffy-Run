using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    // 스테이지 클리어하면 "ClearStage(스테이지 번호)" 메서드를 호출해주세요.
    // 그러면 스테이지 선택 화면에서 
    // 다음 스테이지의 barrier가 사라지고 게임 플레이가 가능해집니다!

    // todo 
    // 3별 평가(?) 기능 구현
    // 
    
    public bool[] stageCleared = new bool[5];
    public GameObject[] stageBarriers = new GameObject[4];

    public void Start(){
        for (int i = 0 ; i <stageBarriers.Length ; i++){
            stageBarriers[i] = GameObject.Find("Stage" + (i + 2) + "/UnclearBarrier");
        }
    }

    public void Update(){

        for (int i = 0; i < stageCleared.Length - 1; i++)
        {
            if (stageCleared[i])
            {
                ClearStage(i + 1);
            }

            else {
                stageBarriers[i].SetActive(true);
            }
        }
    }
    
    public void OnStage(int stageIndex)
    {
        if (stageIndex > 0 && stageIndex <= stageCleared.Length)
        {
            int sceneIndex = stageIndex;

            if (sceneIndex == 1) 
            {
                SceneManager.LoadScene("Stage" + (sceneIndex));
            }
            else if (stageCleared[stageIndex - 2])
            {
                SceneManager.LoadScene("Stage" + (sceneIndex));
                Debug.Log("Stage" + stageIndex);
            }
            else
            {
                Debug.Log("Stage " + (stageIndex - 1) + " must be cleared first");
            }
        }
        else
        {
            Debug.Log("Invalid stage index");
        }
    }

    public void ClearStage(int stageIndex)
    {
        if (stageIndex > 0 && stageIndex < stageCleared.Length)
        {
            stageCleared[stageIndex - 1] = true;
            stageBarriers[stageIndex - 1].SetActive(false); // 다음 스테이지 barrier 비활성화
        }
        else if (stageIndex == stageCleared.Length)
        {
            // 마지막 스테이지(Stage5) 클리어
            stageCleared[stageIndex - 1] = true;
        }
        else
        {
            Debug.Log("Invalid stage index");
        }
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
        
    }
}
