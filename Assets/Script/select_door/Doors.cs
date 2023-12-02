using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;



public class Doors : MonoBehaviour
{
    public DoorGenerator doorGenerator;
    public Start_game SG;
    public GameObject OVRPlayerController;
    
    public GameObject regularPrefab;
    public GameObject randLPrefab;
    public GameObject randomBoxPrefab;
    public GameObject recoveryPrefab;
    public GameObject UpstateHPPrefab;
    public GameObject UpstateDMPrefab;
    public EnemySpawner ES;

    public void ChangeDoorLocation(DoorName[] selectedDoors, int i)
    {
        string selectedtag = SG.AllDoors[i].Tag;
        float planeHeight = 0f;  // Plane�� ���̸� �����ϴ� ����, �ʱ�ȭ�� 0���� ����
        float planeX = 0f;
        float planeY = 0f;

        GameObject floorPlane = GameObject.FindGameObjectWithTag(selectedtag);
        if (floorPlane != null)
        {
            Renderer planeRenderer = floorPlane.GetComponent<Renderer>();
            if (planeRenderer != null)
            {
                planeHeight = planeRenderer.bounds.max.z;
                planeX = planeRenderer.bounds.center.x;
                planeY = planeRenderer.bounds.center.y;
            }
        }

        selectedDoors = SG.SelectTopTwoDoors(SG.AllDoors);

        // ���õ� ���� ���� DoorInfo ����
        DoorInfo leftDoorInfo = new DoorInfo(selectedDoors[0].Name, selectedDoors[0].Tag, new UnityEngine.Vector3(planeX - 2f, planeY + 1.5f, planeHeight));
        DoorInfo rightDoorInfo = new DoorInfo(selectedDoors[1].Name, selectedDoors[1].Tag, new UnityEngine.Vector3(planeX + 2f, planeY + 1.5f, planeHeight));

        // �� ���� ���� DoorGenerator ȣ��
        doorGenerator.GenerateDoor(leftDoorInfo);
        doorGenerator.GenerateDoor(rightDoorInfo);

        // ���õ� ���� �̸� �ܼ� ���
        Debug.Log("Left Door: " + leftDoorInfo.Name + "|| Right Door: " + rightDoorInfo.Name);
    }

    public void MovePlayer(int i)
    {
        string selectedtag = SG.AllDoors[i].Tag;

        // ��� GameObject ��Ȱ��ȭ
        regularPrefab.SetActive(false);
        randLPrefab.SetActive(false);
        randomBoxPrefab.SetActive(false);
        recoveryPrefab.SetActive(false);
        UpstateHPPrefab.SetActive(false);
        UpstateDMPrefab.SetActive(false);

        PlayerController PC = FindObjectOfType<PlayerController>();
        PlayerStatus PS = FindAnyObjectByType<PlayerStatus>();
        
        // Collider ��Ȱ��ȭ
        Collider treasureBoxCollider = null;

        if (selectedtag == "Treasure_Floor")
        {
            PS.checkBox = true;
        }
        if (selectedtag == "Trap_Floor")
        {
            PS.checkBox = false;
        }

        switch (selectedtag)
        {
            case "Regular_Floor":
                regularPrefab.SetActive(true);
                ES.SpawnEnemies();
                break;
            case "Recovery_Floor":
                randLPrefab.SetActive(true);
                recoveryPrefab.SetActive(true);
                break;
            case "Lintel_Floor":
                randLPrefab.SetActive(true);
                UpstateHPPrefab.SetActive(true);
                UpstateDMPrefab.SetActive(true);
                PC.HPupEffect.Stop();
                PC.DMupEffect.Stop();
                break;
            case "Treasure_Floor":
            case "Trap_Floor":
                randomBoxPrefab.SetActive(true);
                PC.debuff_boxEF.Stop();
                PC.open_boxEF.Stop();
                PC.buff_boxEF.Stop();
                GameObject treasureBox = GameObject.FindWithTag("TreasureBox_OB");
                if (treasureBox != null)
                {
                    treasureBoxCollider = treasureBox.GetComponent<Collider>();
                    if (treasureBoxCollider != null)
                    {
                        treasureBoxCollider.enabled = true;
                    }
                }
                break;
        }

        // �ʿ��� ��� Collider Ȱ��ȭ
        if (treasureBoxCollider != null)
        {
            treasureBoxCollider.enabled = true;
        }


        float planeHeight = 0f;  // Plane�� ���̸� �����ϴ� ����, �ʱ�ȭ�� 0���� ����
        float planeX = 0f;
        float planeY = 0f;
        GameObject floorPlane = GameObject.FindGameObjectWithTag(selectedtag);
        if (floorPlane != null)
        {
            Renderer planeRenderer = floorPlane.GetComponent<Renderer>();
            if (planeRenderer != null)
            {
                planeHeight = planeRenderer.bounds.min.z + 5f;
                planeX = planeRenderer.bounds.center.x;
                planeY = planeRenderer.bounds.center.y;
            }
        }
        DisableCharacterController();
        OVRPlayerController.transform.localPosition = new UnityEngine.Vector3(planeX, planeY, planeHeight);
        EnableCharacterController();

    }

    void DisableCharacterController()
    {
        // CharacterController�� ������ �ִ��� Ȯ��
        CharacterController characterController = GetComponent<CharacterController>();

        if (characterController != null)
        {
            // CharacterController�� ��Ȱ��ȭ
            characterController.enabled = false;
        }
        else
        {
            Debug.LogWarning("CharacterController not found on this GameObject.");
        }
    }

    void EnableCharacterController()
    {
        // CharacterController�� ������ �ִ��� Ȯ��
        CharacterController characterController = GetComponent<CharacterController>();

        if (characterController != null)
        {
            // CharacterController�� Ȱ��ȭ
            characterController.enabled = true;
        }
        else
        {
            Debug.LogWarning("CharacterController not found on this GameObject.");
        }
    }
}
