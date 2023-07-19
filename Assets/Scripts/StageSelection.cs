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

    public bool[] stageCleared = new bool[6]; // 스테이지별 클리어 유무
    public int[] clearResult = new int[]{0,0,0,0,0,0}; // 스테이지별 클리어 결과(3별 평가 결과)
    public GameObject[] stageBarriers = new GameObject[5]; // 스테이지별 베리어 오브젝트
    public GameObject[] stageTitles = new GameObject[6]; // 스테이지 타이틀 오브젝트
    public Sprite clearStageSprite; // 클리어된 스테이지 타이틀 스프라이트
    public GameObject allClearObject; // 올클리어 오브젝트

    public void Start(){
        for (int i = 0 ; i <stageBarriers.Length ; i++)
        {
            stageBarriers[i] = GameObject.Find("Stage" + (i + 2) + "/UnclearBarrier");
        }
        for (int i = 0 ; i <stageTitles.Length ; i++)
        {
            stageTitles[i] = GameObject.Find("Stage" + (i + 1) + "/StageTitle");
        }

        allClearObject = GameObject.Find("AllClear");
        allClearObject.SetActive(false);

        clearStageSprite =  Resources.Load <Sprite>("Sprites/UI/clear_stage"); 
        if (!clearStageSprite) {
            Debug.Log("Clear stage sprite not found");
        }

        ClearStage(1,3); // 작동 확인용(추후 삭제될 예정)
    }

    public void Update(){
        // 코드의 정상 작동 확인을 위한 로직(추후 삭제될 예정)
        // i = 0 ~ 4(5개)
        for (int i = 0; i < stageCleared.Length - 1; i++)
        {
            if (stageCleared[i])
            {
                ClearStage(i + 1,0);
            }

            else {
                stageBarriers[i].SetActive(true);
            }
        }
        if (stageCleared[5])
        {
            ClearStage(6, 2);
        }
    }
    
    // 다음 스테이지로 이동하는 로직
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

    // 스테이지 Clear 후 행해지는 로직
    // 1. 해당 스테이지 clear 표시 후 스테이지 타이틀 색상 변경
    // 2. 다음 스테이지의 베리어 비활성화
    // 3. 해당 스테이지의 3별 표시하기

    // ** 마지막 스테이지는 클리어 후 AllClear 오브젝트 표시
    public void ClearStage(int stageIndex, int result)
    {
        if (stageIndex > 0 && stageIndex <= stageCleared.Length)
        {
            // 해당 스테이지 clear 처리
            stageCleared[stageIndex - 1] = true;  
            
            // 스테이지 색상변경
            GameObject stageTitleObj = stageTitles[stageIndex - 1];
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
            if (stageIndex == stageCleared.Length)
            {
                StartCoroutine(ShowClearObjectWithDelay());
                Debug.Log("Clear All Stage!");
            }

            else
            {
                // 스테이지 1 ~ 5에만 적용
                stageBarriers[stageIndex - 1].SetActive(false); // 다음 스테이지 barrier 비활성화
            }
            
            // 3별 표시
            if (0 < result && result < 4)
            {
                clearResult[stageIndex-1] = result; // 클리어 결과 저장(3,2,1 중 하나로)
                Debug.Log("[Stage : " + stageIndex + "] 3 star result : " + result);

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
            Debug.Log("Invalid stage index");
            OnClickExit();
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
