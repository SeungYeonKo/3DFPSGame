using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 역할 : 게임 관리자
// -> 게임 전체의 상태를 알리고, 시작과 끝을 텍스트로 나타낸다.

public enum GameState
{
    Ready,          // 준비
    Start,           // 시작
    Over,           // 오버
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

   // 게임의 상태는 처음에 준비 상태
    public GameState state { get; private set; } = GameState.Ready;

    public Text StateTextUI;

    public Color ReadyStateColor;
    public Color StartStateColor;
    public Color OverStateColor;

    public void Awake()
    {
        Instance = this;
    }
   
    private void Start()
    {
        StartCoroutine(Start_Coroutine());
    }

    private IEnumerator Start_Coroutine()
    {
        // 게임 상태
        // 1. 게임 준비 상태
        state = GameState.Ready;
        StateTextUI.gameObject.SetActive(true);
        Refresh();

        // 2. 1.6초 후에 게임 시작 상태
        yield return new WaitForSeconds(1.6f);
        state = GameState.Start;
        Refresh();

        // 3. 0.4초 후에 텍스트 사라지고
        yield return new WaitForSeconds(0.4f);
        StateTextUI.gameObject.SetActive(false);
    }

    // 4. 플레이를 하다가 
    // 5. 플레이어 체력이 0 이 되면 "게임 오버" 상태
    public void GameOver()
    {
            state = GameState.Over;
            StateTextUI.gameObject.SetActive(true);
            Refresh();
    }

    public void Refresh()
    {
        switch(state)
        {
            case GameState.Ready:
            {
                StateTextUI.text = "Ready . . .";
                StateTextUI.color = ReadyStateColor;
                break;
            }
            case GameState.Start:
            {
                StateTextUI.text = "Start ~";
                StateTextUI.color = StartStateColor;

                break;
            }
            case GameState.Over:
            {
                StateTextUI.text = "Over . . .";
                StateTextUI.color = OverStateColor;

                break;
            }
        }
    }

}
