using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : MonoBehaviour
{
    public void DieEnemy()
    {
        EnemyStatus ES = FindObjectOfType<EnemyStatus>();
        ES.EnemyDeath();
        Destroy(transform.root.gameObject);
    }
}
