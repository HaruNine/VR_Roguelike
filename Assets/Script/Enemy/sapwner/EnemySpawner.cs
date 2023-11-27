using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;  // ù ��° �� ������
    public GameObject enemyPrefab2;  // �� ��° �� ������
    public GameObject enemyPrefab3;  // �� ��° �� ������
    public GameObject enemyPrefab4;  // �� ��° �� ������

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
                else { additionalEnemies = Mathf.Clamp(Random.Range(-3, 13), 0, 12); Debug.Log("����Ȯ��2 : " + additionalEnemies); }

                // ��� ������ ť����� ����Ʈ
                List<Transform> availableCubes = new List<Transform>();

                // ��� Cube�� ã�Ƽ� ����Ʈ�� �߰�
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
                        // ������ ť�� ����
                        int randomIndex = Random.Range(0, availableCubes.Count);
                        Transform selectedCube = availableCubes[randomIndex];

                        // ���� ��ġ
                        Vector3 spawnPosition = selectedCube.position;

                        // ������ �� ������ ����
                        int randomEnemyPrefabIndex = Random.Range(1, 5);  // 1���� 4����
                        GameObject selectedEnemyPrefab = GetEnemyPrefab(randomEnemyPrefabIndex);

                        // Enemy�� �����ϰ� spawnPosition�� ��ġ��Ŵ
                        GameObject enemy = Instantiate(selectedEnemyPrefab, spawnPosition, Quaternion.identity);

                        // ���� ť��� ����Ʈ���� ����
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

    // DestroyEnemys() �޼���� �״�� ����մϴ�.
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
