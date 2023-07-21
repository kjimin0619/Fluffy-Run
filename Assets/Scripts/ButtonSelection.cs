using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSelection : MonoBehaviour
{
    void Start() {
        
    }

    void Update() {
        
    }

    public string sceneName = "StageSelectScene";
    public void OnClickStart()
    {
        Debug.Log("start");
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickRetry()
    {
        Debug.Log("Retry Pressed");

        int stageNum = PlayerPrefs.GetInt("currentPlayingStage", 0);
        SceneManager.LoadScene(stageNum is >= 1 and <= 6 ? $"Stage{stageNum}" : "StageSelectScene");
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
