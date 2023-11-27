using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CthulhuDie : MonoBehaviour
{
    public void DieEnemy()
    {
        CthulhuStatus CS = FindObjectOfType<CthulhuStatus>();
        CS.EnemyDeath();
        Destroy(transform.root.gameObject);
    }
}
