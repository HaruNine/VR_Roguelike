using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class Ghost : MonoBehaviour
{
    private PlayerStatusUI playerstatusUI;
    public GhostStatus GS;

    // ������
    private Transform target;

    // ���
    private NavMeshAgent agent;

    public float distanceToPlayer;
    private float attackRange = 12f;

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

        Vector3 targetPosition = target.position;

        distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (GS.enemyHP <= 0)
        {
            GS.SetState(GhostState.Die);
        }

        switch (GS.currentState)
        {
            case GhostState.Idle:
                // Idle ���¿� ���� ó��
                break;
            case GhostState.Move:
                GS.ani.SetBool("stand", false);
                GS.ani.SetBool("atk", false);
                GS.ani.SetBool("walk", true);
                if (distanceToPlayer >= attackRange)
                {
                    agent.destination = targetPosition;
                }
                if (distanceToPlayer <= attackRange)
                {
                    GS.SetState(GhostState.Attack);
                }
                break;
            case GhostState.Attack:
                GS.ani.SetBool("walk", false);
                GS.ani.SetBool("stand", true);
                GS.ani.SetBool("atk", true);
                if (distanceToPlayer <= attackRange)
                {
                    agent.destination = transform.position;
                }
                else
                {
                    GS.SetState(GhostState.Move);
                }
                break;
            case GhostState.Die:
                GS.ani.SetBool("die", true);
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
            GS.TakeDamage(playerstatusUI.playerStatus.playerDamage);

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
