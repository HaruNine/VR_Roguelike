using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer_Ghost : MonoBehaviour
{
    public GhostStatus GS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GS.currentState != GhostState.Die)
        {
            GS.SetState(GhostState.Move);
        }
    }
}
