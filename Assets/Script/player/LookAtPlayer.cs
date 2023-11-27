using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    void Update()
    {
        // mainCamera 태그를 가진 카메라를 찾습니다.
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (mainCamera != null)
        {
            // mainCamera를 바라보도록 회전을 계산
            Vector3 lookDirection = mainCamera.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

            // 회전 적용
            transform.rotation = rotation;
        }
        else
        {
            Debug.LogWarning("MainCamera not found.");
        }
    }
}
