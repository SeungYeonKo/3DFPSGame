using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Rifle,
    Sniper,
    Pistol
}

public class Gun : MonoBehaviour
{
    public GunType GType;

    // - 공격력
    public int Damage = 10;

    // - 발사 쿨타임
    public float FireCooltime = 0.2f;

    // - 총알 개수
    public int BulletRemainCount;
    public int BulletMaxCount = 30;

    // - 재장전 시간
    public  float ReloadTime = 1.5f;

    // - 대표 이미지
    public Sprite ProfileImage;

    private void Start()
    {
        // 총알 개수 초기화
        BulletRemainCount = BulletMaxCount;
    }
    public void SetVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
