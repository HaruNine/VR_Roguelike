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

    // 목적지
    private Transform target;

    // 요원
    private NavMeshAgent agent;

    public float distanceToPlayer;
    private float attackRange = 2f;
    private float forwardTo = 1.35f;

    // Start is called before the first frame update
    void Start()
    {
        // 요원을 정의
        agent = GetComponent<NavMeshAgent>();

        // 생성될 때 목적지(Player)를 찾음
        target = GameObject.Find("OVRPlayerController").transform;

        // PlayerStatusUI 스크립트 가져오기
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
                // Idle 상태에 대한 처리
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


    // 충돌 감지
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 Long Sword인지 확인
        if (other.CompareTag("LongSword"))
        {
            // HP를 감소시키고 체크
            FS.TakeDamage(playerstatusUI.playerStatus.playerDamage);

            // 오른손 컨트롤러에 진동 적용
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
