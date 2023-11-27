using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    public EnemyStatus ES;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ES.currentState != EnemyState.Die)
        {
            ES.SetState(EnemyState.Move);
        }
    }
}
