using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;

    // Todo 1. 아이템 프리팹을 3개만든다. 
    // Todo 2. 플레이어와 일정 거리가 되면 아이템이 먹어지고 사라진다
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // 아이템 매니저(인벤토리)에 추가하고, 
            ItemManager.Instance.AddItem(ItemType);
            // 사라진다
            gameObject.SetActive(false);
        }
    }
}
    // 실습 과제 31. 몬스터가 죽으면 아이템이 드랍(Health: 30%, Stamina: 20%, Bullet: 10%)
    // 실습 과제 32.  일정 거리가 되면 아이템이 베지어 곡선으로 날라오게 하기 (중간점 랜덤)


