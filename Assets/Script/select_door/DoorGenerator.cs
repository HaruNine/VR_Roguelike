using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public Vector3 Position { get; set; }

    // 생성자를 수정하여 Position을 Vector3로 받도록 변경
    public DoorInfo(string name, string tag, Vector3 position)
    {
        Name = name;
        Tag = tag;
        Position = position;
    }

    // ToString 메서드 오버라이드
    public override string ToString()
    {
        return $"DoorInfo: Name - {Name}, Position - {Position}";
    }
}

public class DoorGenerator : MonoBehaviour
{
    // Assign your door prefab in the Inspector
    public GameObject doorPrefab;
    public GameObject textPrefab; // 추가된 부분: 문 이름을 표시할 텍스트 프리팹

    // 생성된 문에 대한 참조를 저장할 리스트
    private List<GameObject> generatedDoors = new List<GameObject>();
    private List<GameObject> generatedTexts = new List<GameObject>(); // 추가된 부분: 생성된 텍스트 오브젝트를 추적하기 위한 리스트

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

        // 문에 대한 참조를 리스트에 추가
        generatedDoors.Add(doorObject);
        generatedTexts.Add(textObject);
        // Additional initialization or manipulation if needed
    }

    // 생성된 문을 없애는 함수
    public void DestroyDoor(GameObject doorObject)
    {
        Destroy(doorObject);

        // 제거된 문에 대한 텍스트도 제거
        int index = generatedDoors.IndexOf(doorObject);
        if (index >= 0 && index < generatedTexts.Count)
        {
            Destroy(generatedTexts[index]);
            generatedTexts.RemoveAt(index);
        }

        generatedDoors.Remove(doorObject);
    }

    // 생성된 모든 문을 없애는 함수
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
