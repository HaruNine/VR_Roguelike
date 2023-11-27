using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    // 충돌 감지
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fireball"))
        {
            PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
            GhostStatus ghostStatus = FindObjectOfType<GhostStatus>();
            // HP를 감소시키고 체크
            if (ghostStatus != null) { playerStatus.TakeDamage(ghostStatus.enemyDamage); }
        }
    }
}
