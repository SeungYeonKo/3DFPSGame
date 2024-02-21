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
        // 플레이어와 나의 거리를 알고 싶다
        float distance = Vector3.Distance(collider. transform.position, transform.position);
        if (collider.CompareTag("Player"))
        {
            if (ItemManager.Instance != null)
            {
                // 아이템 매니저(인벤토리)에 추가하고, 
                switch (ItemType)
                {
                    case ItemType.Health:
                        ItemManager.Instance.AddItem(ItemType.Health);
                        break;
                    case ItemType.Stamina:
                        ItemManager.Instance.AddItem(ItemType.Stamina);
                        break;
                    case ItemType.Bullet:
                        ItemManager.Instance.AddItem(ItemType.Bullet);
                        break;
                }
                ItemManager.Instance.RefreshUI();
                // 사라진다
                Destroy(gameObject);
               
            }
            else
            {
                Debug.LogWarning("ItemManager is not found!");
            }
        }
    }
    // 실습 과제 31. 몬스터가 죽으면 아이템이 드랍(Health: 30%, Stamina: 20%, Bullet: 10%)
    // 실습 과제 32.  일정 거리가 되면 아이템이 베지어 곡선으로 날라오게 하기 (중간점 랜덤)

}
