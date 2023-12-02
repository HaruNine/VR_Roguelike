using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour
{
    private PlayerStatusUI playerstatusUI;
    public EnemyStatus ES;

    // ������
    private Transform target;

    // ���
    private NavMeshAgent agent;

    public float distanceToPlayer;
    private float attackRange = 1.7f;
    private float forwardTo = 1.35f;

    public GameObject UserSoundManager;


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

        UserSoundManager = GameObject.Find("OVRPlayerController");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);

        Vector3 targetPosition = target.position + target.forward * forwardTo;

        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (ES.enemyHP <= 0)
        {
            ES.SetState(EnemyState.Die);
        }

        switch (ES.currentState)
        {
            case EnemyState.Idle:
                // Idle ���¿� ���� ó��
                break;
            case EnemyState.Move:
                ES.ani.SetBool("stand", false);
                ES.ani.SetBool("atk", false);
                ES.ani.SetBool("walk", true);
                if (distanceToPlayer >= attackRange)
                {
                    agent.destination = targetPosition;
                }
                if (distanceToPlayer <= attackRange)
                {
                    ES.SetState(EnemyState.Attack);
                }
                break;
            case EnemyState.Attack:
                ES.ani.SetBool("walk", false);
                ES.ani.SetBool("stand", true);
                ES.ani.SetBool("atk", true);
                if (distanceToPlayer <= attackRange)
                {
                    agent.destination = transform.position;
                }
                else
                {
                    ES.SetState(EnemyState.Move);
                }
                break;
            case EnemyState.Die:
                ES.ani.SetBool("die", true);
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
            ES.TakeDamage(playerstatusUI.playerStatus.playerDamage);

            // ������ ��Ʈ�ѷ��� ���� ����
            StartCoroutine(Haptics(1, 1, 0.03f, true, true));

            UserSoundManager.GetComponent<UserSoundManager>().PlayAtkSound();
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
