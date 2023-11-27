using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDie : MonoBehaviour
{
    public void DieEnemy()
    {
        GhostStatus GS = FindObjectOfType<GhostStatus>();
        GS.EnemyDeath();
        Destroy(transform.root.gameObject);
    }
}
