using Meta.WitAi;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour
{
    public Start_game SG;
    public Doors Doors;
    public DoorName[] AllDoors;

    public ParticleSystem HPupEffect;
    public ParticleSystem DMupEffect;
    public ParticleSystem HealEffect;
    public ParticleSystem open_boxEF;
    public ParticleSystem debuff_boxEF;
    public ParticleSystem buff_boxEF;

    public GameObject playerstatusUI;
    private bool isUIActive = false;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HealEffect.Stop();
        
    }

    void Update()
    {
        // 오른손 그랩 버튼을 누르는지 확인
        bool isGrabbing = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

        if (isGrabbing)
        {
            LongSwordController longSword = FindObjectOfType<LongSwordController>();
            longSword.SetVisibilityAndCollider(isGrabbing);
        }
        else
        {
            // 그랩을 해제할 때 콜라이더를 끄고 투명도를 조절
            LongSwordController longSword = FindObjectOfType<LongSwordController>();
            longSword.SetVisibilityAndCollider(false);
        }


        // 오른쪽 컨트롤러 검지 트리거 클릭 감지
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // 검지 트리거 클릭 시 동작 수행
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            // Ray와 충돌한 경우
            if (Physics.Raycast(ray, out hit, 5f))
            {
                // 충돌한 객체의 태그가 "Door"인지 확인
                if (hit.collider.CompareTag("Door"))
                {
                    // 클릭한 문의 이름
                    string clickedDoorName = hit.collider.gameObject.name;

                    // 이름에 따라 동작 구분
                    if (clickedDoorName == SG.AllDoors[0].Name)
                    {
                        selectDoor(0);
                    }
                    else if (clickedDoorName == SG.AllDoors[1].Name)
                    {
                        selectDoor(1);
                    }
                    PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
                    playerStatus.Player_Clear();
                }
                if (hit.collider.CompareTag("Recovery_OB"))
                {
                    StartCoroutine(ChangeObjectSizeOverTime(hit.collider.gameObject));
                    PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
                    if (playerStatus != null)
                    {
                        HealEffect.Play();
                        playerStatus.RecoveryHP();
                    }
                }
                if (hit.collider.CompareTag("Up_MaxHP"))
                {
                    StartCoroutine(ChangeObjectSizeOverTime(hit.collider.gameObject));
                    PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
                    if (playerStatus != null)
                    {
                        HPupEffect.Play();
                        if(playerStatus.playerSoul >= playerStatus.paySoulHP)
                        {
                            playerStatus.UP_MaxHP();
                        }
                    }
                }
                if (hit.collider.CompareTag("Up_Damage"))
                {
                    StartCoroutine(ChangeObjectSizeOverTime(hit.collider.gameObject));
                    PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
                    if (playerStatus != null)
                    {
                        DMupEffect.Play();
                        if (playerStatus.playerSoul >= playerStatus.paySoulDM)
                        {
                            playerStatus.UP_playerDamage();
                        }
                    }
                }
                if (hit.collider.CompareTag("TreasureBox_OB"))
                {
                    StartCoroutine(ChangeObjectSizeOverTime(hit.collider.gameObject));
                    PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
                    open_boxEF.Play();
                    if (playerStatus != null)
                    {
                        playerStatus.RandomSelectChest();
                    }
                    hit.collider.enabled = false;
                }
            }
        }

        // 왼쪽 컨트롤러 x버튼 클릭 감지
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            // playerstatusUI 토글
            if (playerstatusUI != null)
            {
                // Canvas 비활성화
                Canvas canvas = playerstatusUI.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = !canvas.enabled;
                }

                // 모든 MeshRenderer 컴포넌트를 찾아서 활성화 또는 비활성화
                MeshRenderer[] allMeshRenderers = playerstatusUI.GetComponentsInChildren<MeshRenderer>(true);
                foreach (MeshRenderer meshRenderer in allMeshRenderers)
                {
                    meshRenderer.enabled = isUIActive;
                }

                isUIActive = !isUIActive;
            }
        }
    }

    void selectDoor(int i)
    {
        // 선택된 문의 이름 출력
        Debug.Log("Click to Selected Door: " + SG.AllDoors[i].Name + " | Tag = " + SG.AllDoors[i].Tag);
        SG.doorGenerator.DestroyAllDoors();
        Doors.MovePlayer(i);
        Doors.ChangeDoorLocation(SG.AllDoors, i);
    }

    IEnumerator ChangeObjectSizeOverTime(GameObject obj)
    {
        float currentTime = 0f;
        float Duration = 0.05f;
        Vector3 initialScale = obj.transform.localScale;
        Vector3 increase = new Vector3(0.02f, 0.02f, 0.02f);

        // 크기 키우기
        while (currentTime < Duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, initialScale + increase, currentTime / Duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // 크기 줄이기
        currentTime = 0f;
        while (currentTime < Duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale + increase, initialScale, currentTime / Duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // 크기를 초기 크기로 설정 (반드시 설정해주어야 함)
        obj.transform.localScale = initialScale;
    }
}
