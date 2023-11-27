using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    void Update()
    {
        // mainCamera �±׸� ���� ī�޶� ã���ϴ�.
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCamera != null)
        {
            // mainCamera�� �ٶ󺸵��� ȸ���� ���
            Vector3 lookDirection = mainCamera.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            // ȸ�� ����
            transform.rotation = rotation;
        }
        else
        {
            Debug.LogWarning("MainCamera not found.");
        }
    }
}
