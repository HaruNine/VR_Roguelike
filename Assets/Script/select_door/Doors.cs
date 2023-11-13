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
    public LandformSpawner LS;
    public EnemySpawner ES;

    public void ChangeDoorLocation(DoorName[] selectedDoors, int i)
    {
        string selectedtag = SG.AllDoors[i].Tag;
        float planeHeight = 0f;  // Plane�� ���̸� �����ϴ� ����, �ʱ�ȭ�� 0���� ����
        float planeX = 0f;

        GameObject floorPlane = GameObject.FindGameObjectWithTag(selectedtag);
        if (floorPlane != null)
        {
            Renderer planeRenderer = floorPlane.GetComponent<Renderer>();
            if (planeRenderer != null)
            {
                planeHeight = planeRenderer.bounds.max.z;
                planeX = planeRenderer.bounds.center.x;
            }
        }

        selectedDoors = SG.SelectTopTwoDoors(SG.AllDoors);

        // ���õ� ���� ���� DoorInfo ����
        DoorInfo leftDoorInfo = new DoorInfo(selectedDoors[0].Name, selectedDoors[0].Tag, new UnityEngine.Vector3(planeX - 2f, 1.5f, planeHeight));
        DoorInfo rightDoorInfo = new DoorInfo(selectedDoors[1].Name, selectedDoors[1].Tag, new UnityEngine.Vector3(planeX + 2f, 1.5f, planeHeight));

        // �� ���� ���� DoorGenerator ȣ��
        doorGenerator.GenerateDoor(leftDoorInfo);
        doorGenerator.GenerateDoor(rightDoorInfo);

        // ���õ� ���� �̸� �ܼ� ���
        Debug.Log("Left Door: " + leftDoorInfo.Name + "|| Right Door: " + rightDoorInfo.Name);
    }

    public void MovePlayer(int i)
    {
        string selectedtag = SG.AllDoors[i].Tag;
        if (selectedtag == "Regular_Floor")
        {
            LS.DestroyLandforms();
            LS.SpawnLandforms();
            ES.DestroyEnemys();
            ES.SpawnEnemies();
        }
        else { LS.DestroyLandforms(); ES.DestroyEnemys(); }
            
        float planeHeight = 0f;  // Plane�� ���̸� �����ϴ� ����, �ʱ�ȭ�� 0���� ����
        float planeX = 0f;
        GameObject floorPlane = GameObject.FindGameObjectWithTag(selectedtag);
        if (floorPlane != null)
        {
            Renderer planeRenderer = floorPlane.GetComponent<Renderer>();
            if (planeRenderer != null)
            {
                planeHeight = planeRenderer.bounds.min.z + 5f;
                planeX = planeRenderer.bounds.center.x;
            }
        }
        DisableCharacterController();
        // Set the player's position to the clicked door's position with X and Z set to zero
        OVRPlayerController.transform.localPosition = new UnityEngine.Vector3(planeX, 2f, planeHeight);
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
