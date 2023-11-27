using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    // �浹 ����
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fireball"))
        {
            PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
            GhostStatus ghostStatus = FindObjectOfType<GhostStatus>();
            // HP�� ���ҽ�Ű�� üũ
            if (ghostStatus != null) { playerStatus.TakeDamage(ghostStatus.enemyDamage); }
        }
    }
}
