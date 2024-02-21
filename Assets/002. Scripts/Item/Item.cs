using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public enum ItemType
{
    Health,          // 체력 회복
    Stamina,       // 스태미나 회복 
    Bullet            // 총알 충전
}

public class Item 
{
    public ItemType ItemType;
    public int Count;

    public Item(ItemType itemType, int count)
    {
        ItemType = itemType;
        Count = count;
    }   

    public bool TryUse()
    {
        if(Count == 0)
        {
            return false;
        }
        Count -= 1;

        switch(ItemType)
        {
            case ItemType.Health:
            {
                // Todo : 플레이어 체력 회복
                PlayerMoveAbility playerMoveAbility = GameObject.FindWithTag("Player").GetComponent<PlayerMoveAbility>();
                playerMoveAbility.Health = playerMoveAbility.MaxHealth;
                break;
            }
            case ItemType.Stamina:
            {
                // Todo : 플레이어 스태미나 회복
                PlayerMoveAbility playerMoveAbility = GameObject.FindWithTag("Player").GetComponent<PlayerMoveAbility>();
                playerMoveAbility.Stamina = playerMoveAbility.MaxStamina;
                break;
            }
            case ItemType.Bullet:
            {
                // Todo : 플레이어 총알 충전
                PlayerGunFireAbility ability = GameObject.FindWithTag("Player").GetComponent<PlayerGunFireAbility>();
                ability.CurrentGun.BulletRemainCount = ability.CurrentGun.BulletMaxCount;
                ability.RefreshUI();
                break;
            }
        }
        return true;
    }
}
