using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemState
{
    Idle,
    Flying
}
public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;
   
    public Transform _target;
    public float FindDistance = 7f;
    public float MoveSpeed = 15f;

    private CharacterController _characterController;

    private ItemState _CurrentState = ItemState.Idle;
    // Todo 1. 아이템 프리팹을 3개만든다. 
    // Todo 2. 플레이어와 일정 거리가 되면 아이템이 먹어지고 사라진다

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        switch (_CurrentState)
        {
            case ItemState.Idle:
                Idle();
                break;
            case ItemState.Flying:
                Flying();
                break;
        }
    }
    private void Idle()
    {
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환 : Idle -> Flying");
            _CurrentState = ItemState.Flying;
        }
    }

    private void Flying()
    {
        Vector3 dir = _target.transform.position - this.transform.position;
        dir.Normalize();

        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        transform.LookAt(_target);
        Debug.Log("슝~ 날아가는중~");
    }

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


