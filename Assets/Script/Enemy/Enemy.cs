using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // 목적지
    private Transform target;

    // 요원
    private NavMeshAgent agent;

    // 플레이어와의 거리
    public float distanceAhead = 1.35f;

    // 적의 HP
    public int enemyHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        // 요원을 정의
        agent = GetComponent<NavMeshAgent>();

        // 생성될 때 목적지(Player)를 찾음
        target = GameObject.Find("OVRPlayerController").transform;

        // 요원에게 초기 목적지 설정
        SetDestinationToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어가 존재하면 목적지로 설정
        if (target != null)
        {
            // 플레이어 위치에서 일정 거리만큼 앞으로 이동한 지점을 목적지로 설정
            Vector3 targetPosition = target.position + target.forward * distanceAhead;
            agent.destination = targetPosition;
        }
        else
        {
            Debug.LogWarning("Player transform not set in the inspector.");
        }
    }

    // 플레이어를 목적지로 설정하는 함수
    void SetDestinationToPlayer()
    {
        // 플레이어가 존재하면 목적지로 설정
        if (target != null)
        {
            // 플레이어 위치에서 일정 거리만큼 앞으로 이동한 지점을 목적지로 설정
            Vector3 targetPosition = target.position + target.forward * distanceAhead;
            agent.destination = targetPosition;
        }
        else
        {
            Debug.LogWarning("Player transform not set in the inspector.");
        }
    }

    // 충돌 감지
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 Long Sword인지 확인
        if (other.CompareTag("LongSword"))
        {
            // HP를 감소시키고 체크
            enemyHP -= 20;

            // HP가 0 이하면 적을 제거
            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
