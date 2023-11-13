using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Start_game SG;
    public GameObject enemyPrefab;  // Enemy 프리팹을 인스펙터에서 설정할 변수



    public void SpawnEnemies()
    {
        if (enemyPrefab != null)
        {
            // 하위에 있는 모든 Cube를 찾음
            Transform[] cubes = GetComponentsInChildren<Transform>();

            // 각 Cube에 대해 스폰
            foreach (Transform cubeTransform in cubes)
            {
                if (cubeTransform != transform)  // 자기 자신은 제외
                {
                    // Cube의 이름에서 숫자 부분을 추출하여 Enemy 번호로 사용
                    string cubeName = cubeTransform.name;
                    string numberString = cubeName.Substring(1);  // "E1"에서 "1" 추출
                    int enemyNumber;

                    if (int.TryParse(numberString, out enemyNumber))
                    {
                        // 스폰 위치
                        Vector3 spawnPosition = cubeTransform.position;

                        // Enemy를 생성하고 spawnPosition에 위치시킴
                        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                        // 생성된 Enemy의 부모를 현재 스포너로 설정
                        enemy.transform.parent = transform;

                        // 필요에 따라 추가적인 설정 가능
                        // 예: enemy.GetComponent<EnemyScript>().Initialize();
                    }
                    else
                    {
                        Debug.LogWarning($"Invalid cube name format for {cubeName}. Cube names should be in the format 'E1', 'E2', ..., 'E12'.");
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Enemy Prefab not assigned to the spawner.");
        }
    }
    public void DestroyEnemys()
    {
        // 현재 스포너의 하위에 있는 모든 Landform(Clone) 오브젝트 제거
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Enemy(Clone)"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
