using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Player_Ghost : MonoBehaviour
{
    public GameObject fireBallPrefab;  // Fire_Ball 프리팹을 인스펙터에서 설정할 변수

    public void AttackPlayer()
    {
        if (fireBallPrefab != null)
        {
            // Fire_Ball을 생성하고 현재 위치에서 발사
            GameObject fireBall = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);

            // 플레이어 쪽으로 발사되도록 방향 설정 (예: 플레이어의 위치를 타겟으로 설정)
            GameObject player = GameObject.FindGameObjectWithTag("MainCamera");
            if (player != null)
            {
                Vector3 direction = (player.transform.position - transform.position).normalized;
                fireBall.GetComponent<Rigidbody>().velocity = direction * 5f;  // 적절한 속도로 설정
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
