using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1인칭 슈팅 (First Person Shooter)

public class FPSCamera : MonoBehaviour
{
    // 목표 :마우스를 조작하면 카메라를 그 방향으로 회전시키고 싶다
    // 필요 속성 : 회전 속도
    public float RotationSpeed = 200;       // 초당 200도까지 회전 가능한 속도
    // 순서 :
    // 1. 마우스 입력(drag) 을 받는다.
    // 2. 마우스 입력 값을 이용해 회전 방향을 구한다.
    // 3. 회전 방향으로 회전한다.

    private void Update()
    {
        // 1. 마우스 입력(drag) 을 받는다.
        float mouseX = Input.GetAxis("Mouse X");                      // 방향에 따라 -1 ~ 1 사이의 값 반환
        float mouseY = Input.GetAxis("Mouse Y");
        //Debug.Log($"GetAxis :{mouseX},{mouseY}");
        //Vector2 mousePosition = Input.mousePosition;          // 진짜 마우스 좌표값
        //Debug.Log($"mousePosition : {mousePosition.x},{mousePosition.y}");

        // 2. 마우스 입력 값을 이용해 회전 방향을 구한다.
        Vector3 rotationDir = new Vector3(-mouseY, mouseX, 0);        // x,y,z
        rotationDir.Normalize();                                                            // 정규화

        // 3. 회전 방향으로 회전한다.
        // 새로운 위치 = 이전 위치 + 방향 * 속도 * 시간
        // 새로운 회전 = 이전 회전 + 방향 * 속도 * 시간
        transform.eulerAngles += rotationDir * RotationSpeed * Time.deltaTime;   
        //=transform.eulerAngles = transform.eulerAngles + rotationDir * RotationSpeed * Time.deltaTime
    }
}
