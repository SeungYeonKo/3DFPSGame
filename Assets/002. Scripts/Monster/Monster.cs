using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum MonsterState            // Monster의 상태
{
    Idle,                   // 유휴
    Trace,                // 대기
    Attack,              //공격
    Comeback,              // 복귀
    Damaged,         // 공격 당함
    Die                    //사망
}

public class Monster : MonoBehaviour, IHitable
{
    private CharacterController _characterController;

    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;
    public Slider HealthSliderUI;

    public Transform _target;                   // 플레이어
    public float FindDistance = 5f;          // 감지 거리
    public float AttackDistance = 2f;       // 공격 범위 
    public float MoveSpeed = 2.5f;         // 이동 속도
    public Vector3 StartPosition;            // Monster 시작 위치
    public float MoveDistance = 40f;     // 움직일 수 있는 거리
    public const float Tolerance = 0.1f;

    public float rotationSpeed = 5f;

    private MonsterState _CurrentState = MonsterState.Idle;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        StartPosition = transform.position;

        Init();
    }

    public void Init()
    {
        Health = MaxHealth;
    }

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

            case MonsterState.Comeback:
                Comeback();
                break;
        }
    }

    private void Idle()
    {
        // todo : 몬스터의 Idle 애니메이션 재생

        if(Vector3.Distance(_target.position, transform.position)<= FindDistance)
        {
            Debug.Log("상태 전환 : Idle -> Trace");
            _CurrentState = MonsterState.Trace;
        }
    }

    private void Trace()
    {
        // 플레이어에게 다가간다
        // 1. 방향을 구한다
        Vector3 dir = _target.transform.position - this.transform.position;
        dir.y = 0;
        dir.Normalize();
        // 2. 이동한다
        _characterController.Move( dir * MoveSpeed * Time.deltaTime);
        // 3. 쳐다본다
        transform.LookAt(_target);

        /*if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
   {
       Debug.Log("상태 전환 : Trace -> Attack");
       _CurrentState = MonsterState.Attack;
   }*/

        if (Vector3.Distance(transform.position, StartPosition) >= MoveDistance)
        {
            Debug.Log("상태 전환 : Trace -> Comeback");
            _CurrentState= MonsterState.Comeback;
        }
    }

    private void Comeback()
    {
        // 1. 방향을 구한다
        Vector3 dir = StartPosition - this.transform.position;
        dir.y = 0;
        dir.Normalize();
        // 2. 이동한다
        _characterController.Move(dir * MoveSpeed * Time.deltaTime);
        RotateCharacter(StartPosition);

        if (Vector3.Distance(this.gameObject.transform.position,  StartPosition) < Tolerance)
        {
            _CurrentState = MonsterState.Idle;
            Debug.Log("상태 전환 : Comeback -> Idle");
        }
    }

    // 어디에 부딪혀도 넘어지지않게 함
    // 주어진 좌표를 기준으로 캐릭터를 회전시키는 함수
    public void RotateCharacter(Vector3 targetPosition)
    {
        // 캐릭터의 위치를 기준으로 회전을 계산
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f; // y 값은 회전에 영향을 미치지 않도록 설정

        // 목표 방향의 Euler 각도 계산
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // 회전 적용
        transform.eulerAngles = new Vector3(0, targetAngle, 0);
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
}