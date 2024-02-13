using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목표 : 키보드 방향키(wasd)를 누르면 캐릭터가 바라보는 방향으로 이동시키고 싶다
    // 속성 : 
    // - 이동속도
    public float MoveSpeed = 3;
    public float RunSpeed   = 10;

    // 스태미너와 관련된 변수
    public float currentStamina;    // 현재 스태미너
    public float minStamina = 0;
    public float maxStamina = 10;

    // 구현 순서:
    // 1. 키 입력 받기
    // 2. '캐릭터가 바라보는 방향'을 기준으로 방향 구하기
    // 3. 이동하기
    void Start()
    {
        MoveSpeed = 3;
        currentStamina = maxStamina; // 시작 시 스태미너를 최대치로 설정
    }

    void Update()
    {
        // 1. 키 입력 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 2.  '캐릭터가 바라보는 방향'을 기준으로 방향 구하기
        Vector3 dir = new Vector3(h, 0, v);     // 로컬 좌표계 (나만의 동서남북)
        dir.Normalize();
        // Transforms direction from local space to world space.
        dir = Camera.main.transform.TransformDirection(dir);  // 글로벌 좌표계 (세상의 동서남북)

        // 3. 이동하기
        transform.position += MoveSpeed * dir * Time.deltaTime;

        // 현재 스태미너를 minStamina와 maxStamina 사이의 값으로 제한
        currentStamina = Mathf.Clamp(currentStamina, minStamina, maxStamina);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveSpeed = 10;
            currentStamina -= Time.deltaTime;
        }
        else
        {
            MoveSpeed = 3;
            currentStamina += Time.deltaTime * 2;
        }
        if(currentStamina <= 0)
        {
            MoveSpeed = 3;
        }
    }
}

