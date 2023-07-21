using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StageSelection : MonoBehaviour
{
    // 스테이지 클리어하면 "ClearStage(스테이지 번호, 클리어결과(3,2,1 중 하나))" 메서드를 호출해주세요.
    // ex. ClearStage(1,3) 
    // 그러면 스테이지 선택 화면에서 
    // 다음 스테이지의 barrier가 사라지고 3별이 표시되며 게임 플레이가 가능해집니다!

    public const int StageCount = 6;
    
    public bool[] stageCleared = new bool[StageCount + 1]; // 스테이지별 클리어 유무
    public GameObject[] stageBarriers = new GameObject[StageCount + 1]; // 스테이지별 베리어 오브젝트
    public GameObject[] stageTitles = new GameObject[StageCount + 1]; // 스테이지 타이틀 오브젝트
    public Sprite clearStageSprite; // 클리어된 스테이지 타이틀 스프라이트
    public GameObject allClearObject; // 올클리어 오브젝트

    public void Start(){
        // 베리어, 타이틀 설정
        for (int i = 2; i <= StageCount; i++)
        {
            stageBarriers[i] = GameObject.Find($"Stage{i}/UnclearBarrier");
        }
        for (int i = 1; i <= StageCount; i++)
        {
            stageTitles[i] = GameObject.Find($"Stage{i}/StageTitle");
        }

        // 올클리어 오브젝트 우선 비활성화
        allClearObject = GameObject.Find("AllClear");
        allClearObject.SetActive(false);

        clearStageSprite =  Resources.Load<Sprite>("Sprites/UI/clear_stage"); 
        if (!clearStageSprite) {
            Debug.Log("Clear stage sprite not found");
        }

        // 저장된 별 갯수 불러와서 1개 이상이면 클리어 처리
        for (int num = 1; num <= StageCount; num++)
        {
            int currentStar = PlayerPrefs.GetInt($"Stage{num}", 0);

            if (currentStar > 0)
            {
                ClearStage(num, currentStar);
            }
        }
    }

    public void Update() {
    }
    
    // 다음 스테이지로 이동하는 로직
    public void OnStage(int stageIndex)
    {
        switch (stageIndex)
        {
            // 스테이지 1이나 직전 스테이지가 클리어되어있는 2~6스테이지 > 진입
            case 1:
            case >= 2 and <= StageCount when stageCleared[stageIndex - 1]:
                Time.timeScale = 1f;
                SceneManager.LoadScene($"Stage{stageIndex}");
                break;
            // 직전 스테이지가 미클리어인 2~6스테이지 > 미진입
            case >= 2 and <= StageCount:
                Debug.Log($"Stage {stageIndex - 1} 선행 클리어 필요");
                break;
            default:
                Debug.Log("잘못된 인덱스");
                break;
        }
    }

    // 스테이지 Clear 후 행해지는 로직
    // 1. 해당 스테이지 clear 표시 후 스테이지 타이틀 색상 변경
    // 2. 다음 스테이지의 베리어 비활성화
    // 3. 해당 스테이지의 3별 표시하기

    // ** 마지막 스테이지는 클리어 후 AllClear 오브젝트 표시
    public void ClearStage(int stageIndex, int result)
    {
        if (stageIndex is >= 1 and <= StageCount)
        {
            // 해당 스테이지 clear 처리
            stageCleared[stageIndex] = true;  
            
            // 스테이지 색상변경
            GameObject stageTitleObj = stageTitles[stageIndex];
            if (stageTitleObj != null)
            {
                Image stageTitleImage = stageTitleObj.GetComponent<Image>();
                if (stageTitleImage != null)
                {
                    stageTitleImage.sprite = clearStageSprite;
                }
                else
                {
                    Debug.LogError("Image component not found on stageTitle object");
                }
            }

            // stage 6(마지막 스테이지) 클리어시 all clear 표시 추가
            if (stageIndex == StageCount)
            {
                StartCoroutine(ShowClearObjectWithDelay());
                Debug.Log("Clear All Stage!");
            }
            else
            {
                // 스테이지 1 ~ 5에만 적용: 다음 스테이지 barrier 비활성화
                stageBarriers[stageIndex + 1].SetActive(false);
            }
            
            // 3별 표시
            if (result is >= 1 and <= 3)
            {
                // 결과에 해당하는 별 프리팹 가져오기
                GameObject starPrefab = Resources.Load<GameObject>("Sprites/UI/Star"+result);
                if (starPrefab != null)
                {
                    GameObject clearResultObj = GameObject.Find("Stage" + stageIndex + "/StageTitle/ClearResult");
                    if (clearResultObj == null)
                    {
                        clearResultObj = new GameObject("clearResult");
                    }

                    GameObject starObj = Instantiate(starPrefab, clearResultObj.transform);
                    starObj.name = "Star_" + stageIndex;
                }   
                else 
                {
                    Debug.LogError("Star Prefab is not found.");
                }
            }
        }
        else
        {
            Debug.Log("Invalid Stage Index");
        }
    }
    
    private IEnumerator ShowClearObjectWithDelay()
    {
        yield return new WaitForSeconds(1f); // 1초 후 오브젝트 등장 
        allClearObject.SetActive(true);
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
