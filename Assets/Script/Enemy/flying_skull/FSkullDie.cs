using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSkullDie : MonoBehaviour
{
    public void DieEnemy()
    {
        FSkullStatus FS = FindObjectOfType<FSkullStatus>();
        FS.EnemyDeath();
        Destroy(transform.root.gameObject);
    }
}
