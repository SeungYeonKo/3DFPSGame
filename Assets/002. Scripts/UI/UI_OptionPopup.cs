using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_OptionPopup : MonoBehaviour
{
    public Button Resume;
    public Button Again;
    public Button End;

    public void OnResumeButtonClicked()
    {
       Debug.Log("계속하기~!!");
        Close();
      
    }
    public void OnAgainButtonClicked()
    {
        Debug.Log("다시하기~!!");
        Close ();
    }

    public void OnEndButtonClicked()
    {
        Debug.Log("게임종료~!!");
         Close ();
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
