using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATK_Player_FSkull : MonoBehaviour
{
    public void AttackPlayer()
    {
        FSkullStatus FS = FindObjectOfType<FSkullStatus>();
        PlayerStatus playerStatus = FindObjectOfType<PlayerStatus>();
        playerStatus.TakeDamage(FS.enemyDamage);
    }
}
