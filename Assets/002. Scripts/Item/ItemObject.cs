using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemState
{
    Idle,           // 대기 상태        (플레이어와의 거리를 체크한다)
    Flying        // 날라오는 상태 (n초에 걸쳐서 slerp로 플레이어에게 날라온다)
}
public class ItemObject : MonoBehaviour
{
    public ItemType ItemType;

    private ItemState _CurrentState = ItemState.Idle;
    public Transform _player;
    public float FindDistance = 7f;           // 감지 거리  

    private Vector3 _startPosition;
    private const float FLYING_DURATION = 0.3f;
    private float _progress = 0;


    // Todo 1. 아이템 프리팹을 3개만든다. 
    // Todo 2. 플레이어와 일정 거리가 되면 아이템이 먹어지고 사라진다

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;     // 캐싱
        _startPosition = transform.position;
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
        // 대기 상태의 행동 : 플레이어와의 거리를 체크한다
        float distance = Vector3.Distance(_player.position, transform.position);
        // 전이 조건 : 플레이어와 거리가 충분히 가까워지면
        if (distance <= FindDistance)
        {
            Debug.Log("상태 전환 : Idle -> Flying");
            _CurrentState = ItemState.Flying;
        }
    }

    private Coroutine _flyingCoroutine;

    private void Flying()
    {
        if (_flyingCoroutine == null)
        {
            _flyingCoroutine = StartCoroutine(Flying_Coroutine());
        }
    }
    public void Init()
    {
        _flyingCoroutine = null;
        _progress = 0;
    }

    private IEnumerator Flying_Coroutine()
    {
        while (_progress < 0.7f)
        {
            _progress += Time.deltaTime / FLYING_DURATION;
            transform.position = Vector3.Slerp(_startPosition, _player.position, _progress);
            yield return null;
        }

        // 1. 아이템 매니저(인벤토리)에 추가하고
        ItemManager.Instance.AddItem(ItemType);
        ItemManager.Instance.RefreshUI();
        // 2. 사라진다
        gameObject.SetActive(false);
    }
}

// 실습 과제 31. 몬스터가 죽으면 아이템이 드랍(Health: 30%, Stamina: 20%, Bullet: 10%)
// 실습 과제 32.  일정 거리가 되면 아이템이 베지어 곡선으로 날라오게 하기 (중간점 랜덤)
/* 
 *  private void Flying()
    {
        _progress += Time.deltaTime / FLYING_DURATION;
        transform.position = Vector3.Slerp(_startPosition, _player.position, _progress);

        if (_progress >= 1)
        {
            // 아이템 매니저(인벤토리)에 추가하고, 
            ItemManager.Instance.AddItem(ItemType);
            // 사라진다
            gameObject.SetActive(false);
        }
        Debug.Log("슝~ 날아가는중~");
    }
*/

