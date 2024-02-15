using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 마우스 오른쪽 버튼을 누르면 시선이 바라보는 방향으로 수류탄을 던지고 싶다
    // 필요 속성 : 
    // - 수류탄 프리팹
    public GameObject BombPrefab;
    // - 수류탄 던지는 위치
    public Transform FirePosition;
    // - 수류탄을 던지는 파워
    public float ThrowPower = 15f;

    [Header("BombCountUI")]
    public Text BombCountTextUI;
    public int BombScore;
    public const int MaxBombScore = 3;


    private void Start()
    {
        BombScore = MaxBombScore;
    }

    private void Update()
    {
        // 수류탄 투척
        // 구현 순서 : 
        // 1. 마우스 오른쪽 버튼을 눌렀을 때 && 수류탄 개수가 0보다 크다면
        if (Input.GetMouseButtonDown(1) && BombScore > 0)         //마우스 오른쪽 1번, 왼쪽 0번, 휠 2번
        {
                // 2. 수류탄 던지는 위치에 수류탄 생성
                GameObject bomb = Instantiate(BombPrefab);
                bomb.transform.position = FirePosition.position;

                // 3. 시선이 바라보는 방향(카메라가 바라보는 방향 = 카메라의 전방)으로 수류탄 투척
                Rigidbody rigidbody = bomb.GetComponent<Rigidbody>();
                rigidbody.AddForce(Camera.main.transform.forward * ThrowPower, ForceMode.Impulse);       //AddForce(방향 * 힘)

                // 수류탄 개수 감소
                BombScore--;
        }
        // UI 업데이트
        BombCountTextUI.text = BombScore + "/" + MaxBombScore;
    }
}
