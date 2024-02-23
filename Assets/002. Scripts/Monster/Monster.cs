using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.VisualScripting;


public enum MonsterState            // Monster의 상태
{
    Idle,                      // 유휴
    Trace,                   // 대기
    Attack,                 //공격
    Comeback,          // 복귀
    Damaged,           // 공격 당함
    Die,                     //사망
    Patrol                 // 순찰
}

public class Monster : MonoBehaviour, IHitable
{
    
    private CharacterController _characterController;

    [Range(0, 100)]
    public int Health;
    public int MaxHealth = 100;
    public Slider HealthSliderUI;

    public Transform _target;                           // 플레이어
    public float FindDistance = 10f;                  // 감지 거리
    public float AttackDistance = 2f;              // 공격 범위 
    public float MoveSpeed = 2.5f;                // 이동 속도
    public Vector3 StartPosition;                    // Monster 시작 위치
    public float MoveDistance = 50f;            // 움직일 수 있는 거리
    public const float Tolerance = 0.1f;         // 허용 오차 범위
    public int Damage = 10;                          // Monster공격력
    public const float AttackDelay = 1f; 
    private float _attackTimer = 0f;

    public Transform PatrolTarget;
    private float _idleTimer;
    private const float IDLE_DURATION = 3;
    
    private Vector3 _KnockbackStartPosition;
    private Vector3 _KnockbackEndPosition;
    private const float KNOCKBACK_DURATION = 0.5f;
    private float _knockbackProgress = 0f;
    public float KnockbackPower = 0.5f;

    private MonsterState _CurrentState = MonsterState.Idle;

    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = MoveSpeed;

        _target = GameObject.FindGameObjectWithTag("Player").transform;
        StartPosition = transform.position;

        Init();
    }

    public void Init()
    {
        _idleTimer = 0f;
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

            case MonsterState.Attack:
                Attack();
                break;

            case MonsterState.Damaged:
                Damaged();
                break;

            case MonsterState.Patrol:
                Patrol();
                break;
        }
    }

    private void Idle()
    {
        _idleTimer += Time.deltaTime;

        // todo : 몬스터의 Idle 애니메이션 재생
        if (Vector3.Distance(_target.position, transform.position)<= FindDistance)
        {
            Debug.Log("상태 전환 : Idle -> Trace");
            _CurrentState = MonsterState.Trace;
        }

        if (PatrolTarget != null && _idleTimer >= IDLE_DURATION)
        {
            _idleTimer = 0f;
            Debug.Log("상태 전환 : Idle -> Patrol");
            _CurrentState = MonsterState.Patrol;
        }
    }

    private void Trace()
    {
        // Trace 상태일때의 행동 코드를 작성

        // 플레이어게 다가간다.
        // 1. 방향을 구한다. (target - me)
        Vector3 dir = _target.transform.position - this.transform.position;
        dir.y = 0;
        dir.Normalize();
        // 2. 이동한다.
        // _characterController.Move(dir * MoveSpeed * Time.deltaTime);

        // 내비게이션이 접근하는 최소 거리를 공격 가능 거리로 설정
        _navMeshAgent.stoppingDistance = AttackDistance;

        // 내비게이션의 목적지를 플레이어의 위치로 한다.
        _navMeshAgent.destination = _target.position;

        // 3. 쳐다본다.
        //transform.forward = dir; //(_target);

        if (Vector3.Distance(transform.position, StartPosition) >= MoveDistance)
        {
            Debug.Log("상태 전환: Trace -> Comeback");
            _CurrentState = MonsterState.Comeback;
        }

        if (Vector3.Distance(_target.position, transform.position) <= AttackDistance)
        {
            Debug.Log("상태 전환: Trace -> Attack");
            _CurrentState = MonsterState.Attack;
        }
    }

    private void Comeback()
    {
        // 실습 과제 34. 복귀 상태의 행동 구현하기:
        // 시작 지점 쳐다보면서 시작지점으로 이동하기 (이동 완료하면 다시 Idle 상태로 전환)
        // 1. 방향을 구한다. (target - me)
        Vector3 dir = StartPosition - this.transform.position;
        dir.y = 0;
        dir.Normalize();
        // 2. 이동한다.
        //_characterController.Move(dir * MoveSpeed * Time.deltaTime);
        // 3. 쳐다본다.
        //transform.forward = dir; //(_target);

        // 내비게이션이 접근하는 최소 거리를 오차 범위
        _navMeshAgent.stoppingDistance = Tolerance;

        // 내비게이션의 목적지를 플레이어의 위치로 한다.
        _navMeshAgent.destination = StartPosition;

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= Tolerance)
        {
            Debug.Log("상태 전환: Comeback -> idle");
            _CurrentState = MonsterState.Idle;
        }

        if (Vector3.Distance(StartPosition, transform.position) <= Tolerance)
        {
            Debug.Log("상태 전환: Comeback -> idle");
            _CurrentState = MonsterState.Idle;
        }
        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환: Comeback -> Trace");
            _CurrentState = MonsterState.Trace;
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

    private void Attack()
    {
        // 전이 사건 : 플레이어와 거리가 공격 범위보다 멀어지면 다시 Trace
        if(Vector3.Distance(_target.position, transform.position)>AttackDistance)
        {
            _attackTimer = 0;
            Debug.Log("상태 전환 : Attack -> Trace");
            _CurrentState = MonsterState.Trace;
            return;
        }
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= AttackDelay) 
        {
            IHitable playerHitable = _target.GetComponent<IHitable>();
            if (playerHitable != null)
            {
                Debug.Log("맞았다!");
                playerHitable.Hit(Damage);
                _attackTimer = 0;
            }
        }
    }

    private void Damaged()
    {
        // 1. Damaged 애니메이션 실행(0.5초)
        // 2. 넉백 구현
        // 2-1. 넉백 시작/최종 위치를 구한다
        if(_knockbackProgress == 0)
        {
            _KnockbackStartPosition = transform.position;
            Vector3 dir = transform.position - _target.position;
            dir.y = 0;
            dir.Normalize();
            _KnockbackEndPosition = transform.position + dir * KnockbackPower;
        }
        _knockbackProgress += Time.deltaTime / KNOCKBACK_DURATION;

        // 2-2. Lerp를 이용해 넉백하기
        transform.position = Vector3.Lerp(_KnockbackStartPosition, _KnockbackEndPosition, _knockbackProgress);

        if(_knockbackProgress > 1)
        {
            _knockbackProgress = 0;
            // 3. Trace로 상태 전환
            Debug.Log("상태 전환 : Damaged -> Trace");
            _CurrentState = MonsterState.Trace;
        }
    }

    public void Patrol()
    {
        _navMeshAgent.stoppingDistance = 0f;
        _navMeshAgent.SetDestination(PatrolTarget.position);

        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= Tolerance)
        {
            Debug.Log("상태 전환: Patrol -> Comeback");
            _CurrentState = MonsterState.Comeback;
        }

        if (Vector3.Distance(_target.position, transform.position) <= FindDistance)
        {
            Debug.Log("상태 전환: Patrol -> Trace");
            _CurrentState = MonsterState.Trace;
        }
    }

    public void Hit(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("상태 전환 : Any -> Damaged");
            _CurrentState = MonsterState.Damaged;
        }
    }

    private void Die()
    {
        // 죽을 때 아이템 생성
        ItemObjectFactory.Instance.MakePercent(transform.position);
        Destroy(this.gameObject);
    }
}