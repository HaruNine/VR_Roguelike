using UnityEngine;

public class LookAtCenterEyeAnchor : MonoBehaviour
{
    void Update()
    {
        if (OVRInput.GetActiveController() != OVRInput.Controller.None)
        {
            // CenterEyeAnchor를 찾아서 해당 방향으로 회전
            Transform centerEyeAnchor = Camera.main.transform;
            if (centerEyeAnchor != null)
            {
                transform.LookAt(centerEyeAnchor);
            }
        }
    }
}
