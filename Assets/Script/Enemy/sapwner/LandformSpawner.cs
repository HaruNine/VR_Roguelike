using UnityEngine;

public class LandformSpawner : MonoBehaviour
{
    public GameObject landformPrefab;  // Landform �������� �ν����Ϳ��� ������ ����

    public void SpawnLandforms()
    {
        if (landformPrefab != null)
        {
            // ������ �ִ� ��� Cube�� ã��
            Transform[] cubes = GetComponentsInChildren<Transform>();

            // �����ϰ� �� ���� Landform�� �������� ���� (1���� 10���� ����)
            int numberOfLandforms = Random.Range(1, 11);

            // �����ϰ� ���õ� Landform�� �ε����� ������ �迭
            int[] selectedIndices = new int[numberOfLandforms];

            // �����ϰ� Landform�� ����
            for (int i = 0; i < numberOfLandforms; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(1, cubes.Length);
                } while (System.Array.IndexOf(selectedIndices, randomIndex) != -1);

                selectedIndices[i] = randomIndex;

                // ���õ� Cube�� ���� ����
                Transform cubeTransform = cubes[randomIndex];
                // Cube�� �̸����� ���� �κ��� �����Ͽ� Landform ��ȣ�� ���
                string cubeName = cubeTransform.name;
                string numberString = cubeName.Substring(1);  // "L1"���� "1" ����
                int landformNumber;

                if (int.TryParse(numberString, out landformNumber))
                {
                    // ���� ��ġ
                    Vector3 spawnPosition = cubeTransform.position;

                    // Landform�� �����ϰ� spawnPosition�� ��ġ��Ŵ
                    GameObject landform = Instantiate(landformPrefab, spawnPosition, Quaternion.identity);

                    // ������ Landform�� �θ� ���� �����ʷ� ����
                    landform.transform.parent = transform;

                    // �ʿ信 ���� �߰����� ���� ����
                    // ��: landform.GetComponent<LandformScript>().Initialize();
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
        // ���� �������� ������ �ִ� ��� Landform(Clone) ������Ʈ ����
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Landform(Clone)"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
