using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LobbyScene : MonoBehaviour
{
    // 사용자 계정을 새로 저장하거나(회원가입), 저장된 데이터를 읽어(로그인)
    // 사용자 입력과 일치하는지 검사한다
    public TMP_InputField IDInputField;
    public TMP_InputField PWInputField;
    public Text NotifyTextUI;

    private void Start()
    {
        IDInputField.text = string.Empty;
        PWInputField.text = string.Empty;
        NotifyTextUI.text = string.Empty;
    }

    // 회원가입 버튼 클릭
    public void OnClickRegisterButton()
    {
        string id = IDInputField.text;
        string pw = PWInputField.text;
        if (id == string.Empty || pw == string.Empty)
        {
            NotifyTextUI.text = "아이디와 비밀번호를 정확하게 입력해주세요.";
            return;
        }
        // 1. 이미 같은 계정으로 회원가입이 되어있는 경우
        if (PlayerPrefs.HasKey(id))
        {
            NotifyTextUI.text = "이미 존재하는 계정입니다.";
        }
        // 2. 회원가입에 성공하는 경우
        else
        {
            NotifyTextUI.text = "회원가입을 완료했습니다 !";
            PlayerPrefs.SetString(id, pw);
        }
        IDInputField.text = string.Empty;
        PWInputField.text = string.Empty;
    }

    // 로그인 버튼 클릭
    public void OnClickLoginButton()
    {
        string id = IDInputField.text;
        string pw = PWInputField.text;

        // 0. 아이디 또는 비밀번호 입력 x  -> "아이디와 비밀번호를 정확하게 입력해주세요."
        if (IDInputField.text == string.Empty || PWInputField.text == string.Empty)
        {
            NotifyTextUI.text = "아이디와 비밀번호를 정확하게 입력해주세요.";
        }
        else
        {
            // 1. 아이디가 존재하지 않는 경우
            if (!PlayerPrefs.HasKey(id))
            {
                NotifyTextUI.text = "아이디를 확인해주세요.";
            }
            else
            {
                // 2. 아이디는 존재하나, 비밀번호가 일치하지 않는 경우
                if (PlayerPrefs.GetString(id) != pw)
                {
                    NotifyTextUI.text = "비밀번호를 확인해주세요.";
                }
                else
                {
                    // 3. 로그인 성공 -> 메인 씬으로 이동
                    SceneManager.LoadScene("MainScene");
                }
            }
        }
    }
}