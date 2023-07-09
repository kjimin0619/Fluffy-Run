using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelection : MonoBehaviour
{
    public bool stage1Cleared = false;
    public bool stage2Cleared = false;
    public bool stage3Cleared = false;
    public bool stage4Cleared = false;
    public bool stage5Cleared = false;

    public GameObject stage2Barrier;
    public GameObject stage3Barrier;
    public GameObject stage4Barrier;
    public GameObject stage5Barrier;

    public void Start(){
        stage2Barrier = GameObject.Find("Stage2/UnclearBarrier");
        stage3Barrier = GameObject.Find("Stage3/UnclearBarrier");
        stage4Barrier = GameObject.Find("Stage4/UnclearBarrier");
        stage5Barrier = GameObject.Find("Stage5/UnclearBarrier");
    }

    public void Update(){
        bool isStage1Cleared = checkStageClear();

        if (isStage1Cleared){
            Stage1Cleared();
        }
    }
    
    public bool checkStageClear(){
        return stage1Cleared;
    }

    public void OnStage1()
    {
        Debug.Log("stage1");
        SceneManager.LoadScene("Stage1");
    }

    public void OnStage2()
    {
        
        if (stage1Cleared){
            SceneManager.LoadScene("Stage2");
            Debug.Log("stage2");
        }
        else {
            Debug.Log("Stage 1 must be cleared first");
        }
    }

    public void OnStage3()
    {
         if (stage2Cleared){
            SceneManager.LoadScene("Stage3");
            Debug.Log("stage3");
        }
        else {
            Debug.Log("Stage 2 must be cleared first");
        }
    }

    public void OnStage4()
    {
        if (stage3Cleared){
            SceneManager.LoadScene("Stage4");
            Debug.Log("stage4");
        }
        else {
            Debug.Log("Stage 3 must be cleared first");
        }
    }

    public void OnStage5()
    {
        if (stage4Cleared){
            SceneManager.LoadScene("Stage5");
            Debug.Log("stage5");
        }
        else {
            Debug.Log("Stage 4 must be cleared first");
        }
    }

    public void ClearStage1(){
        stage1Cleared = true;
        stage2Barrier.SetActive(false); // stage2 barrier 비활성화
    }

    public void ClearStage2(){
        stage2Cleared = true;
        stage3Barrier.SetActive(false); // stage3 barrier 비활성화
    }

    public void ClearStage3(){
        stage3Cleared = true;
        stage4Barrier.SetActive(false); // stage4 barrier 비활성화
    }

    public void ClearStage4(){
        stage4Cleared = true;
        stage5Barrier.SetActive(false); // stage5 barrier 비활성화
    }

    public void ClearedStage5(){
        stage5Cleared = true;
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
