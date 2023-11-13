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
        float planeHeight = 0f;  // Plane의 높이를 저장하는 변수, 초기화는 0으로 가정
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

        // 선택된 문에 대한 DoorInfo 생성
        DoorInfo leftDoorInfo = new DoorInfo(selectedDoors[0].Name, selectedDoors[0].Tag, new UnityEngine.Vector3(planeX - 2f, 1.5f, planeHeight));
        DoorInfo rightDoorInfo = new DoorInfo(selectedDoors[1].Name, selectedDoors[1].Tag, new UnityEngine.Vector3(planeX + 2f, 1.5f, planeHeight));

        // 각 문에 대한 DoorGenerator 호출
        doorGenerator.GenerateDoor(leftDoorInfo);
        doorGenerator.GenerateDoor(rightDoorInfo);

        // 선택된 문의 이름 콘솔 출력
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
            
        float planeHeight = 0f;  // Plane의 높이를 저장하는 변수, 초기화는 0으로 가정
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
        // CharacterController를 가지고 있는지 확인
        CharacterController characterController = GetComponent<CharacterController>();

        if (characterController != null)
        {
            // CharacterController를 비활성화
            characterController.enabled = false;
        }
        else
        {
            Debug.LogWarning("CharacterController not found on this GameObject.");
        }
    }

    void EnableCharacterController()
    {
        // CharacterController를 가지고 있는지 확인
        CharacterController characterController = GetComponent<CharacterController>();

        if (characterController != null)
        {
            // CharacterController를 활성화
            characterController.enabled = true;
        }
        else
        {
            Debug.LogWarning("CharacterController not found on this GameObject.");
        }
    }
}
