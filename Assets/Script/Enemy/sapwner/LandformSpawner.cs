using UnityEngine;

public class LandformSpawner : MonoBehaviour
{
    public GameObject landformPrefab;  // Landform 프리팹을 인스펙터에서 설정할 변수

    public void SpawnLandforms()
    {
        if (landformPrefab != null)
        {
            // 하위에 있는 모든 Cube를 찾음
            Transform[] cubes = GetComponentsInChildren<Transform>();

            // 랜덤하게 몇 개의 Landform을 스폰할지 선택 (1에서 10까지 랜덤)
            int numberOfLandforms = Random.Range(1, 11);

            // 랜덤하게 선택된 Landform의 인덱스를 저장할 배열
            int[] selectedIndices = new int[numberOfLandforms];

            // 랜덤하게 Landform을 선택
            for (int i = 0; i < numberOfLandforms; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(1, cubes.Length);
                } while (System.Array.IndexOf(selectedIndices, randomIndex) != -1);

                selectedIndices[i] = randomIndex;

                // 선택된 Cube에 대해 스폰
                Transform cubeTransform = cubes[randomIndex];
                // Cube의 이름에서 숫자 부분을 추출하여 Landform 번호로 사용
                string cubeName = cubeTransform.name;
                string numberString = cubeName.Substring(1);  // "L1"에서 "1" 추출
                int landformNumber;

                if (int.TryParse(numberString, out landformNumber))
                {
                    // 스폰 위치
                    Vector3 spawnPosition = cubeTransform.position;

                    // Landform을 생성하고 spawnPosition에 위치시킴
                    GameObject landform = Instantiate(landformPrefab, spawnPosition, Quaternion.identity);

                    // 생성된 Landform의 부모를 현재 스포너로 설정
                    landform.transform.parent = transform;

                    // 필요에 따라 추가적인 설정 가능
                    // 예: landform.GetComponent<LandformScript>().Initialize();
                }
                else
                {
                    Debug.LogWarning($"Invalid cube name format for {cubeName}. Cube names should be in the format 'L1', 'L2', ..., 'L10'.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Landform Prefab not assigned to the spawner.");
        }
    }

    public void DestroyLandforms()
    {
        // 현재 스포너의 하위에 있는 모든 Landform(Clone) 오브젝트 제거
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Landform(Clone)"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
