using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorName
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public float Probability { get; set; }

    // 생성자를 사용하여 초기화
    public DoorName(string name, string tag, float probability)
    {
        Name = name;
        Tag = tag;
        Probability = probability;
    }
}

public class Start_game : MonoBehaviour
{
    DoorName[] StratRoom = new DoorName[]
    {
        new DoorName("Main Room", "Floor", 0f)
    };
    DoorName[] selectedDoors;
    public DoorName[] AllDoors;
    public DoorName[] firstRoom;
    public DoorGenerator doorGenerator;

    // DoorName 클래스를 사용하여 각 문의 이름과 확률 저장
    DoorName[] doorOptions = new DoorName[]
    {
            new DoorName("Recovery Room", "Recovery_Floor", 0.3f),
            new DoorName("Trap Room", "Trap_Floor", 0.3f),
            new DoorName("Lintel", "Lintel_Floor", 0.3f),
            new DoorName("Treasure room", "Treasure_Floor", 0.3f),
            new DoorName("Regular Room", "Regular_Floor", 0.3f)
    };

    // Start is called before the first frame update
    void Start()
    {
        firstRoom = StratRoom;
        PrintProbabilities(doorOptions); // 모든 선택지 보기
        
        // 선택 로직 실행
        selectedDoors = SelectTopTwoDoors(doorOptions);

        CreatDoor(selectedDoors);

    }

    // 확률을 콘솔에 출력하는 함수
    void PrintProbabilities(DoorName[] doorOptions)
    {
        string probabilities = "Probabilities: ";

        foreach (var door in doorOptions)
        {
            probabilities += door.Name + "(" + door.Probability + "), ";
        }

        // 마지막에 콤마 제거
        probabilities = probabilities.TrimEnd(',', ' ');

        Debug.Log(probabilities);
    }

    // 5가지중 2가지 선택후 증감
    public DoorName[] SelectTopTwoDoors(DoorName[] doorOptions)
    {
        // 랜덤으로 정렬
        DoorName[] shuffledDoors = doorOptions.OrderBy(door => UnityEngine.Random.value).ToArray();

        // 상위 두 개 선택
        DoorName[] selectedDoors = { shuffledDoors[0], shuffledDoors[1] };

        // 하위 세 개 선택
        DoorName[] unselectedDoors = { shuffledDoors[2], shuffledDoors[3], shuffledDoors[4] };

        // 상위 두 개 문의 확률 감소
        foreach (var door in selectedDoors)
        {
            door.Probability -= 0.04f;
            door.Probability = Mathf.Max(door.Probability, 0.1f);
        }

        // 하위 세 개 문의 확률 증가
        foreach (var door in unselectedDoors)
        {
            door.Probability += 0.04f;
            door.Probability = Mathf.Min(door.Probability, 0.9f);
        }

        AllDoors = selectedDoors.Concat(unselectedDoors).ToArray();

        Debug.Log("All Doors: " + string.Join(", ", AllDoors.Select(door => door.Name + ": " + door.Probability)));

        return AllDoors;
    }

    //초기 문 생성
    public void CreatDoor(DoorName[] selectedDoors)
    {
        float planeHeight = 0f;  // Plane의 높이를 저장하는 변수, 초기화는 0으로 가정
        float planeX = 0f;

        GameObject floorPlane = GameObject.FindGameObjectWithTag("Floor");
        if (floorPlane != null)
        {
            Renderer planeRenderer = floorPlane.GetComponent<Renderer>();
            if (planeRenderer != null)
            {
                planeHeight = planeRenderer.bounds.max.z;
                planeX = planeRenderer.bounds.center.x;
            }
        }

        // 선택된 문에 대한 DoorInfo 생성
        DoorInfo leftDoorInfo = new DoorInfo(selectedDoors[0].Name, selectedDoors[0].Tag, new UnityEngine.Vector3(planeX - 2f, 1.5f, planeHeight));
        DoorInfo rightDoorInfo = new DoorInfo(selectedDoors[1].Name, selectedDoors[1].Tag, new UnityEngine.Vector3(planeX + 2f, 1.5f, planeHeight));

        // 각 문에 대한 DoorGenerator 호출
        doorGenerator.GenerateDoor(leftDoorInfo);
        doorGenerator.GenerateDoor(rightDoorInfo);

        // 선택된 문의 이름 콘솔 출력
        Debug.Log("Left Door: " + leftDoorInfo.Name + "|| Right Door: " + rightDoorInfo.Name);
    }
}
