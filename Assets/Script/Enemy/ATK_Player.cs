using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Player : MonoBehaviour
{
    public void AttackPlayer()
    {
        EnemyStatus ES = FindObjectOfType<EnemyStatus>();
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        playerStatus.TakeDamage(ES.enemyDamage);
    }
}
