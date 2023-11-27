using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;  // 첫 번째 적 프리팹
    public GameObject enemyPrefab2;  // 두 번째 적 프리팹
    public GameObject enemyPrefab3;  // 세 번째 적 프리팹
    public GameObject enemyPrefab4;  // 네 번째 적 프리팹

    public void SpawnEnemies()
    {
        DestroyEnemys();
        int additionalEnemies;
        if (enemyPrefab1 != null && enemyPrefab2 != null && enemyPrefab3 != null && enemyPrefab4 != null)
        {
            PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
            
            if (playerStatus != null)
            {
                if (playerStatus.playerFloor%100 < 50)
                { additionalEnemies = Mathf.Clamp(Random.Range(-3, 7), 0, 6); }
                else { additionalEnemies = Mathf.Clamp(Random.Range(-3, 13), 0, 12); Debug.Log("적수확인2 : " + additionalEnemies); }

                // 사용 가능한 큐브들의 리스트
                List<Transform> availableCubes = new List<Transform>();

                // 모든 Cube를 찾아서 리스트에 추가
                Transform[] cubes = GetComponentsInChildren<Transform>();
                foreach (Transform cube in cubes)
                {
                    if (cube != transform && cube.name.StartsWith("E"))
                    {
                        availableCubes.Add(cube);
                    }
                }

                for (int i = 0; i < additionalEnemies; i++)
                {
                    if (availableCubes.Count > 0)
                    {
                        // 랜덤한 큐브 선택
                        int randomIndex = Random.Range(0, availableCubes.Count);
                        Transform selectedCube = availableCubes[randomIndex];

                        // 스폰 위치
                        Vector3 spawnPosition = selectedCube.position;

                        // 랜덤한 적 프리팹 선택
                        int randomEnemyPrefabIndex = Random.Range(1, 5);  // 1에서 4까지
                        GameObject selectedEnemyPrefab = GetEnemyPrefab(randomEnemyPrefabIndex);

                        // Enemy를 생성하고 spawnPosition에 위치시킴
                        GameObject enemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);

                        // 사용된 큐브는 리스트에서 제거
                        availableCubes.RemoveAt(randomIndex);
                    }
                    else
                    {
                        Debug.LogWarning("No available cube found for enemy spawn.");
                        break;
                    }
                }
            }
            else
            {
                Debug.LogWarning("PlayerStatus not found.");
            }
        }
        else
        {
            Debug.LogWarning("Enemy Prefabs not assigned to the spawner.");
        }
    }

    private GameObject GetEnemyPrefab(int index)
    {
        switch (index)
        {
            case 1:
                return enemyPrefab1;
            case 2:
                return enemyPrefab2;
            case 3:
                return enemyPrefab3;
            case 4:
                return enemyPrefab4;
            default:
                return null;
        }
    }

    // DestroyEnemys() 메서드는 그대로 사용합니다.
    public void DestroyEnemys()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}
