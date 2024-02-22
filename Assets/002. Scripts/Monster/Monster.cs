using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum MonsterState            // Monster의 상태
{
    Idle,                   // 유휴
    Trace,                // 대기
    Attack,              //공격
    Return,              // 복귀
    Damaged,         // 공격 당함
    Die                    //사망
}

public class Monster : MonoBehaviour, IHitable
{
    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;
    public Slider HealthSliderUI;

    public Transform Target;        // 플레이어
    public float FindDistance = 5;      // 

    private MonsterState _CurrentState = MonsterState.Idle;

    private void Update()
    {
        HealthSliderUI.value = (float)Health / (float)MaxHealth;  // 0 ~ 1

        // 상태 패턴 : 상태에 따라 행동을 다르게 하는 패턴
        // 1. 몬스터가 가질 수 있는 행동에 따라 상태를 나눈다.
        // 2. 상태들이 조건에 따라 자연스럽게 전환(Transition)될 수 있도록 설계한다.
        
        switch(_CurrentState)
        {
            case MonsterState.Idle:
                Idle();
                break;

            case MonsterState.Trace:
                Trace();
                break;
        }
    }

    private void Idle()
    {
        // todo : 몬스터의 Idle 애니메이션 재생

        if(Vector3.Distance(Target.position, transform.position)<= FindDistance)
        {
            Debug.Log("상태 전환 : Idle -> Trace");
            _CurrentState = MonsterState.Trace;
        }
    }
    private void Trace()
    {
        // 플레이어에게 다가간다
        // 이동속도 필요

        if(거리가 공격범위 안이면 )
        {
            _CurrentState = MonsterState.Attack;
        }
        if(원점과 거리가 너무 멀어지면)
        {
            _CurrentState = MonsterState.Return;
        }
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 죽을 때 아이템 생성
        ItemObjectFactory.Instance.MakePercent(transform.position);
        Destroy(this.gameObject);
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        Health = MaxHealth;
    }
}