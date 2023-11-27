using UnityEngine;

public class LookAtCenterEyeAnchor : MonoBehaviour
{
    void Update()
    {
        if (OVRInput.GetActiveController() != OVRInput.Controller.None)
        {
            // CenterEyeAnchor�� ã�Ƽ� �ش� �������� ȸ��
            Transform centerEyeAnchor = Camera.main.transform;
            if (centerEyeAnchor != null)
            {
                transform.LookAt(centerEyeAnchor);
            }
        }
    }
}
