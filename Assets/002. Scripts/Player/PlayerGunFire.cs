using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunFire : MonoBehaviour
{
    // 목표 : 마우스 왼쪽 버튼을 누르면 시선이 바라보는 방향으로 총을 발사하고 싶다
    // 필요 속성 : 총알 튀는 이펙트 프리팹
    public ParticleSystem HitEffect;
    // 구현 순서
    // 1. 만약에 마우스 왼쪽 버튼을 누르면
    // 2. 레이 (광선) 을 생성하고, 위치와 방향을 설정한다
    // 3. 레이를 발사한다
    // 4. 레이가 부딪힌 대상의 정보를 받아온다
    // 5. 만약에 부딪힌 위치에 (총알이 튀는) 이펙트를 생성한다

    void Update()
    {
        // 1. 만약에 마우스 왼쪽 버튼을 누르면
        if (Input.GetMouseButtonDown(0))
        {
            // 2. 레이 (광선) 을 생성하고, 위치와 방향을 설정한다
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            // 3. 레이를 발사한다
            RaycastHit hitInfo;
            // 4. 레이가 부딪힌 대상의 정보를 받아온다
            bool IsHit = Physics.Raycast(ray, out hitInfo);
            if(IsHit)
            {
                // 5. 만약에 부딪힌 위치에 (총알이 튀는) 이펙트를 위치한다
                HitEffect.gameObject.transform.position = hitInfo.point;
                // 6. 이펙트가 쳐다보는 방향을 부딪힌 위치의 법선 벡터로 한다
                HitEffect.gameObject.transform.forward = hitInfo.normal;    //normal : 법선벡터 -> 총알이펙트가 바닥에만 적용되기때문에 법선벡터를 이용해 어디에든 자연스럽게 이펙트 되게 해줌
                HitEffect.Play();
            }
           // Physics.Raycast(ray, out hitInfo);           //cast(쏜다)는 물리엔진(Physics.)안에 있다
        }
    }
}
