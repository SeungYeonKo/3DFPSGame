using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 역할: 아이템들을 관리해주는 관리자
// 데이터 관리 -> 데이터를 생성, 수정, 삭제, 조회(검색)
public class ItemManager : MonoBehaviour
{
    //public UnityEvent OnDataChanged;
    // 관찰자 패턴 (유튜버 패턴)
    // 구독자가 구독하고 있는 유튜버의 상태가 변화할 때마다
    // 유튜버는 구독자에게 이벤트를 통지하고, 구독자들은 이벤트 알림을 받아 적절하게 
    // 행동하는 패턴
    public static ItemManager Instance { get; private set; }

    public Text HealthItemCountTextUI;
    public Text StaminaItemCountTextUI;
    public Text BulletItemCountTextUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Item> ItemList = new List<Item>(); // 아이템 리스트

    private void Start()
    {
        ItemList.Add(new Item(ItemType.Health, 0));  // 0: Health
        ItemList.Add(new Item(ItemType.Stamina, 0)); // 1: Stamina
        ItemList.Add(new Item(ItemType.Bullet, 0));  // 2: Bullet

        Refresh();
        //OnDataChanged.Invoke();
    }

    // 1. 아이템 추가(생성)
    public void AddItem(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                ItemList[i].Count++;
                Refresh();
                /*if(OnDataChanged != null)
                {
                    OnDataChanged.Invoke();
                }*/
                break;
            }
        }
    }

    // 2. 아이템 개수 조회
    public int GetItemCount(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                return ItemList[i].Count;
            }
        }
        return 0;
    }

    // 3. 아이템 사용
    public bool TryUseItem(ItemType itemType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].ItemType == itemType)
            {
                bool result = ItemList[i].TryUse();
                //OnDataChanged.Invoke();
                Refresh();
                return result;
            }
        }
        return false;
    }
    public void Refresh()
    {
        HealthItemCountTextUI.text = $"x{ItemManager.Instance.GetItemCount(ItemType.Health)}";
        StaminaItemCountTextUI.text = $"x{ItemManager.Instance.GetItemCount(ItemType.Stamina)}";
        BulletItemCountTextUI.text = $"x{ItemManager.Instance.GetItemCount(ItemType.Bullet)}";
    }

}