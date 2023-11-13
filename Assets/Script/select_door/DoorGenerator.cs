using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public Vector3 Position { get; set; }

    // �����ڸ� �����Ͽ� Position�� Vector3�� �޵��� ����
    public DoorInfo(string name, string tag, Vector3 position)
    {
        Name = name;
        Tag = tag;
        Position = position;
    }

    // ToString �޼��� �������̵�
    public override string ToString()
    {
        return $"DoorInfo: Name - {Name}, Position - {Position}";
    }
}

public class DoorGenerator : MonoBehaviour
{
    // Assign your door prefab in the Inspector
    public GameObject doorPrefab;
    public GameObject textPrefab; // �߰��� �κ�: �� �̸��� ǥ���� �ؽ�Ʈ ������

    // ������ ���� ���� ������ ������ ����Ʈ
    private List<GameObject> generatedDoors = new List<GameObject>();
    private List<GameObject> generatedTexts = new List<GameObject>(); // �߰��� �κ�: ������ �ؽ�Ʈ ������Ʈ�� �����ϱ� ���� ����Ʈ

    public void GenerateDoor(DoorInfo doorInfo)
    {
        // Instantiate the doorPrefab at the specified position
        GameObject doorObject = Instantiate(doorPrefab, doorInfo.Position, Quaternion.identity);

        // Set the name of the instantiated doorObject
        doorObject.name = doorInfo.Name;

        GameObject textObject = Instantiate(textPrefab, doorInfo.Position + new Vector3(0, 2, 0), Quaternion.identity);
        TextMesh textMesh = textObject.GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = doorInfo.Name;
        }

        // ���� ���� ������ ����Ʈ�� �߰�
        generatedDoors.Add(doorObject);
        generatedTexts.Add(textObject);
        // Additional initialization or manipulation if needed
    }

    // ������ ���� ���ִ� �Լ�
    public void DestroyDoor(GameObject doorObject)
    {
        Destroy(doorObject);

        // ���ŵ� ���� ���� �ؽ�Ʈ�� ����
        int index = generatedDoors.IndexOf(doorObject);
        if (index >= 0 && index < generatedTexts.Count)
        {
            Destroy(generatedTexts[index]);
            generatedTexts.RemoveAt(index);
        }

        generatedDoors.Remove(doorObject);
    }

    // ������ ��� ���� ���ִ� �Լ�
    public void DestroyAllDoors()
    {
        foreach (var door in generatedDoors)
        {
            Destroy(door);
        }

        foreach (var text in generatedTexts)
        {
            Destroy(text);
        }

        generatedDoors.Clear();
        generatedTexts.Clear();
    }
}
