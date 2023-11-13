using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public Start_game SG;
    public GameObject enemyPrefab;  // Enemy �������� �ν����Ϳ��� ������ ����



    public void SpawnEnemies()
    {
        if (enemyPrefab != null)
        {
            // ������ �ִ� ��� Cube�� ã��
            Transform[] cubes = GetComponentsInChildren<Transform>();

            // �� Cube�� ���� ����
            foreach (Transform cubeTransform in cubes)
            {
                if (cubeTransform != transform)  // �ڱ� �ڽ��� ����
                {
                    // Cube�� �̸����� ���� �κ��� �����Ͽ� Enemy ��ȣ�� ���
                    string cubeName = cubeTransform.name;
                    string numberString = cubeName.Substring(1);  // "E1"���� "1" ����
                    int enemyNumber;

                    if (int.TryParse(numberString, out enemyNumber))
                    {
                        // ���� ��ġ
                        Vector3 spawnPosition = cubeTransform.position;

                        // Enemy�� �����ϰ� spawnPosition�� ��ġ��Ŵ
                        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                        // ������ Enemy�� �θ� ���� �����ʷ� ����
                        enemy.transform.parent = transform;

                        // �ʿ信 ���� �߰����� ���� ����
                        // ��: enemy.GetComponent<EnemyScript>().Initialize();
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
        // ���� �������� ������ �ִ� ��� Landform(Clone) ������Ʈ ����
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Enemy(Clone)"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
