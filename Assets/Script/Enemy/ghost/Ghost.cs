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

    // 목적지
    private Transform target;

    // 요원
    private NavMeshAgent agent;

    public float distanceToPlayer;
    private float attackRange = 12f;

    public GameObject UserSoundManager;

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
                // Idle 상태에 대한 처리
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


    // 충돌 감지
    void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 Long Sword인지 확인
        if (other.CompareTag("LongSword"))
        {
            // HP를 감소시키고 체크
            GS.TakeDamage(playerstatusUI.playerStatus.playerDamage);

            // 오른손 컨트롤러에 진동 적용
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
