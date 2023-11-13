using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // ������
    private Transform target;

    // ���
    private NavMeshAgent agent;

    // �÷��̾���� �Ÿ�
    public float distanceAhead = 1.35f;

    // ���� HP
    public int enemyHP = 100;

    // Start is called before the first frame update
    void Start()
    {
        // ����� ����
        agent = GetComponent<NavMeshAgent>();

        // ������ �� ������(Player)�� ã��
        target = GameObject.Find("OVRPlayerController").transform;

        // ������� �ʱ� ������ ����
        SetDestinationToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾ �����ϸ� �������� ����
        if (target != null)
        {
            // �÷��̾� ��ġ���� ���� �Ÿ���ŭ ������ �̵��� ������ �������� ����
            Vector3 targetPosition = target.position + target.forward * distanceAhead;
            agent.destination = targetPosition;
        }
        else
        {
            Debug.LogWarning("Player transform not set in the inspector.");
        }
    }

    // �÷��̾ �������� �����ϴ� �Լ�
    void SetDestinationToPlayer()
    {
        // �÷��̾ �����ϸ� �������� ����
        if (target != null)
        {
            // �÷��̾� ��ġ���� ���� �Ÿ���ŭ ������ �̵��� ������ �������� ����
            Vector3 targetPosition = target.position + target.forward * distanceAhead;
            agent.destination = targetPosition;
        }
        else
        {
            Debug.LogWarning("Player transform not set in the inspector.");
        }
    }

    // �浹 ����
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� Long Sword���� Ȯ��
        if (other.CompareTag("LongSword"))
        {
            // HP�� ���ҽ�Ű�� üũ
            enemyHP -= 20;

            // HP�� 0 ���ϸ� ���� ����
            if (enemyHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
