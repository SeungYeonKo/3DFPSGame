using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOverPopup : MonoBehaviour
{
    public void OnAgainButtonClicked()
    {
        Debug.Log("다시하기~!!");
        // 메인씬을 끄고 다시 불러오는 방법
        //SceneManager.LoadScene("MainScene");    // = SceneManager.LoadScene("0"); 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Close();
    }

    public void OnEndButtonClicked()
    {
        Debug.Log("게임종료~!!");
        // 빌드 후 실행했을 경우 종료하는 방법
        //Application.Quit();

#if UNITY_EDITOR
        // 유니티 에디터에서 실행했을 경우 종료하는 방법
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
