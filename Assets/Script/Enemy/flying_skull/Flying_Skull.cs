using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Flying_Skull : MonoBehaviour
{
    private PlayerStatusUI playerstatusUI;
    public FSkullStatus FS;

    // ������
    private Transform target;

    // ���
    private NavMeshAgent agent;

    public float distanceToPlayer;
    private float attackRange = 2f;
    private float forwardTo = 1.35f;

    // Start is called before the first frame update
    void Start()
    {
        // ����� ����
        agent = GetComponent<NavMeshAgent>();

        // ������ �� ������(Player)�� ã��
        target = GameObject.Find("OVRPlayerController").transform;

        // PlayerStatusUI ��ũ��Ʈ ��������
        playerstatusUI = GameObject.Find("playerstatusUI").GetComponent<PlayerStatusUI>();

        //StartCoroutine(AttackCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);

        Vector3 targetPosition = target.position + target.forward * forwardTo;

        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (FS.enemyHP <= 0)
        {
            FS.SetState(FSkullState.Die);
        }

        switch (FS.currentState)
        {
            case FSkullState.Idle:
                // Idle ���¿� ���� ó��
                break;
            case FSkullState.Move:
                FS.ani.SetBool("stand", false);
                FS.ani.SetBool("atk", false);
                FS.ani.SetBool("walk", true);
                if (distanceToPlayer >= attackRange)
                {
                    agent.destination = targetPosition;
                }
                if (distanceToPlayer <= attackRange)
                {
                    FS.SetState(FSkullState.Attack);
                }
                break;
            case FSkullState.Attack:
                FS.ani.SetBool("walk", false);
                FS.ani.SetBool("stand", true);
                FS.ani.SetBool("atk", true);
                if (distanceToPlayer <= attackRange)
                {
                    agent.destination = transform.position;
                }
                else
                {
                    FS.SetState(FSkullState.Move);
                }
                break;
            case FSkullState.Die:
                FS.ani.SetBool("die", true);
                break;
            default:
                break;
        }
    }


    // �浹 ����
    void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� Long Sword���� Ȯ��
        if (other.CompareTag("LongSword"))
        {
            // HP�� ���ҽ�Ű�� üũ
            FS.TakeDamage(playerstatusUI.playerStatus.playerDamage);

            // ������ ��Ʈ�ѷ��� ���� ����
            StartCoroutine(Haptics(1, 1, 0.03f, true, true));

        }
    }

    IEnumerator Haptics(float frequency, float amplitude, float duration, bool rightHand, bool leftHand)
    {

        if (rightHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.RTouch);
        if (leftHand) OVRInput.SetControllerVibration(frequency, amplitude, OVRInput.Controller.LTouch);

        yield return new WaitForSeconds(duration);

        if (rightHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        if (leftHand) OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }
}