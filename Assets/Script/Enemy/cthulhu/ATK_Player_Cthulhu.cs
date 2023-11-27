using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Player_Cthulhu : MonoBehaviour
{
    public void AttackPlayer()
    {
        CthulhuStatus CS = FindObjectOfType<CthulhuStatus>();
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        playerStatus.TakeDamage(CS.enemyDamage);
    }
}
