using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorName
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public float Probability { get; set; }

    // �����ڸ� ����Ͽ� �ʱ�ȭ
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

    // DoorName Ŭ������ ����Ͽ� �� ���� �̸��� Ȯ�� ����
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
        PrintProbabilities(doorOptions); // ��� ������ ����
        
        // ���� ���� ����
        selectedDoors = SelectTopTwoDoors(doorOptions);

        CreatDoor(selectedDoors);

    }

    // Ȯ���� �ֿܼ� ����ϴ� �Լ�
    void PrintProbabilities(DoorName[] doorOptions)
    {
        string probabilities = "Probabilities: ";

        foreach (var door in doorOptions)
        {
            probabilities += door.Name + "(" + door.Probability + "), ";
        }

        // �������� �޸� ����
        probabilities = probabilities.TrimEnd(',', ' ');

        Debug.Log(probabilities);
    }

    // 5������ 2���� ������ ����
    public DoorName[] SelectTopTwoDoors(DoorName[] doorOptions)
    {
        // �������� ����
        DoorName[] shuffledDoors = doorOptions.OrderBy(door => UnityEngine.Random.value).ToArray();

        // ���� �� �� ����
        DoorName[] selectedDoors = { shuffledDoors[0], shuffledDoors[1] };

        // ���� �� �� ����
        DoorName[] unselectedDoors = { shuffledDoors[2], shuffledDoors[3], shuffledDoors[4] };

        // ���� �� �� ���� Ȯ�� ����
        foreach (var door in selectedDoors)
        {
            door.Probability -= 0.04f;
            door.Probability = Mathf.Max(door.Probability, 0.1f);
        }

        // ���� �� �� ���� Ȯ�� ����
        foreach (var door in unselectedDoors)
        {
            door.Probability += 0.04f;
            door.Probability = Mathf.Min(door.Probability, 0.9f);
        }

        AllDoors = selectedDoors.Concat(unselectedDoors).ToArray();

        Debug.Log("All Doors: " + string.Join(", ", AllDoors.Select(door => door.Name + ": " + door.Probability)));

        return AllDoors;
    }

    //�ʱ� �� ����
    public void CreatDoor(DoorName[] selectedDoors)
    {
        float planeHeight = 0f;  // Plane�� ���̸� �����ϴ� ����, �ʱ�ȭ�� 0���� ����
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

        // ���õ� ���� ���� DoorInfo ����
        DoorInfo leftDoorInfo = new DoorInfo(selectedDoors[0].Name, selectedDoors[0].Tag, new UnityEngine.Vector3(planeX - 2f, 1.5f, planeHeight));
        DoorInfo rightDoorInfo = new DoorInfo(selectedDoors[1].Name, selectedDoors[1].Tag, new UnityEngine.Vector3(planeX + 2f, 1.5f, planeHeight));

        // �� ���� ���� DoorGenerator ȣ��
        doorGenerator.GenerateDoor(leftDoorInfo);
        doorGenerator.GenerateDoor(rightDoorInfo);

        // ���õ� ���� �̸� �ܼ� ���
        Debug.Log("Left Door: " + leftDoorInfo.Name + "|| Right Door: " + rightDoorInfo.Name);
    }
}
