using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    // 스테이지 클리어하면 "ClearStage(스테이지 번호, 클리어결과(3,2,1 중 하나))" 메서드를 호출해주세요.
    // ex. ClearStage(1,3) 
    // 그러면 스테이지 선택 화면에서 
    // 다음 스테이지의 barrier가 사라지고 3별이 표시되며 게임 플레이가 가능해집니다!

    public bool[] stageCleared = new bool[5]; // 스테이지별 클리어 유무
    public int[] clearResult = new int[]{0,0,0,0,0}; // 스테이지별 클리어 결과(3별 평가 결과)
    public GameObject[] stageBarriers = new GameObject[4]; // 스테이지별 베리어 오브젝트
    
    public void Start(){
        for (int i = 0 ; i <stageBarriers.Length ; i++){
            stageBarriers[i] = GameObject.Find("Stage" + (i + 2) + "/UnclearBarrier");
        }
        ClearStage(1,3);
    }

    public void Update(){
        // 코드의 정상 작동 확인을 위한 로직
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
    // 1. 다음 스테이지의 베리어 없애기
    // 2. 해당 스테이지의 3별 표시하기
    public void ClearStage(int stageIndex, int result)
    {
        if (stageIndex > 0 && stageIndex < stageCleared.Length)
        {
            if (stageIndex != stageCleared.Length)
            {
                // 스테이지 1 ~ 4
                stageBarriers[stageIndex - 1].SetActive(false); // 다음 스테이지 barrier 비활성화
            }
            stageCleared[stageIndex - 1] = true; 
            
            // 3별 표시
            if (0 < result && result < 4)
            {
                clearResult[stageIndex-1] = result; // 클리어 결과 저장(3,2,1 중 하나로)
                Debug.Log("3 star result : " + result);

                // 결과에 해당하는 별 프리팹 가져오기
                GameObject starPrefab = Resources.Load<GameObject>("UI/Star"+result);
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
                    Debug.LogError("Prefab is not found in Assets/Source(UI) folder.");
                }

            }
        }   
        else
        {
            Debug.Log("Invalid stage index");
            OnClickExit();
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
