using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPlayer_FSkull : MonoBehaviour
{
    public FSkullStatus FS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && FS.currentState != FSkullState.Die)
        {
            FS.SetState(FSkullState.Move);
        }
    }
}
