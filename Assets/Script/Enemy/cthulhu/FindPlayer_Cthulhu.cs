using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer_Cthulhu : MonoBehaviour
{
    public CthulhuStatus CS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && CS.currentState != CthulhuState.Die)
        {
            CS.SetState(CthulhuState.Move);
        }
    }
}
