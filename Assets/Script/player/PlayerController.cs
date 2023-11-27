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
        // ������ �׷� ��ư�� �������� Ȯ��
        bool isGrabbing = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

        if (isGrabbing)
        {
            LongSwordController longSword = FindObjectOfType<LongSwordController>();
            longSword.SetVisibilityAndCollider(isGrabbing);
        }
        else
        {
            // �׷��� ������ �� �ݶ��̴��� ���� ������ ����
            LongSwordController longSword = FindObjectOfType<LongSwordController>();
            longSword.SetVisibilityAndCollider(false);
        }


        // ������ ��Ʈ�ѷ� ���� Ʈ���� Ŭ�� ����
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            // ���� Ʈ���� Ŭ�� �� ���� ����
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            // Ray�� �浹�� ���
            if (Physics.Raycast(ray, out hit, 5f))
            {
                // �浹�� ��ü�� �±װ� "Door"���� Ȯ��
                if (hit.collider.CompareTag("Door"))
                {
                    // Ŭ���� ���� �̸�
                    string clickedDoorName = hit.collider.gameObject.name;

                    // �̸��� ���� ���� ����
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

        // ���� ��Ʈ�ѷ� x��ư Ŭ�� ����
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            // playerstatusUI ���
            if (playerstatusUI != null)
            {
                // Canvas ��Ȱ��ȭ
                Canvas canvas = playerstatusUI.GetComponentInChildren<Canvas>();
                if (canvas != null)
                {
                    canvas.enabled = !canvas.enabled;
                }

                // ��� MeshRenderer ������Ʈ�� ã�Ƽ� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
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
        // ���õ� ���� �̸� ���
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

        // ũ�� Ű���
        while (currentTime < Duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, initialScale + increase, currentTime / Duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // ũ�� ���̱�
        currentTime = 0f;
        while (currentTime < Duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale + increase, initialScale, currentTime / Duration);
            currentTime += Time.deltaTime;
            yield return null;
        }

        // ũ�⸦ �ʱ� ũ��� ���� (�ݵ�� �������־�� ��)
        obj.transform.localScale = initialScale;
    }
}
