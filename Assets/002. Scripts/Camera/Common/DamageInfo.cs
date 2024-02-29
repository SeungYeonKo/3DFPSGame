using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Normal,                 
    Critical
}

public struct DamageInfo
{
    public DamageType DamageType;
    public int                  Amount;                  // 데미지량
    public Vector3          Position;         
    public Vector3          Normal;                  // 방향(법선벡터)

    public DamageInfo(DamageType damageType ,int amount)
    {
        this.DamageType = damageType;
        this.Amount = amount;
        this.Position = Vector3.zero;
        this.Normal = Vector3.zero;
    }
}
