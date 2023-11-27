using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Player_Ghost : MonoBehaviour
{
    public GameObject fireBallPrefab;  // Fire_Ball �������� �ν����Ϳ��� ������ ����

    public void AttackPlayer()
    {
        if (fireBallPrefab != null)
        {
            // Fire_Ball�� �����ϰ� ���� ��ġ���� �߻�
            GameObject fireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);

            // �÷��̾� ������ �߻�ǵ��� ���� ���� (��: �÷��̾��� ��ġ�� Ÿ������ ����)
            GameObject player = GameObject.FindGameObjectWithTag("MainCamera");
            if (player != null)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                fireBall.GetComponent<Rigidbody>().velocity = direction * 5f;  // ������ �ӵ��� ����
            }
            else
            {
                Debug.LogWarning("Player not found.");
            }
        }
        else
        {
            Debug.LogWarning("Fire_Ball Prefab not assigned.");
        }
    }
}
