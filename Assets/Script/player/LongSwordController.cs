using UnityEngine;
using OculusSampleFramework;

public class LongSwordController : MonoBehaviour
{
    private Collider longSwordCollider;
    private Renderer longSwordRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // "LongSword" ������Ʈ�� Collider�� Renderer ��������
        longSwordCollider = GetComponent<Collider>();
        longSwordRenderer = GetComponent<Renderer>();
    }
    public void SetVisibilityAndCollider(bool isVisible)
    {
        // Collider Ȱ��ȭ ���� ����
        longSwordCollider.enabled = isVisible;

        // ���� ����
        Color currentColor = longSwordRenderer.material.color;
        float targetAlpha = isVisible ? 1.0f : 0.0f;

        // ���ο� ���� ����
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
        longSwordRenderer.material.color = newColor;
    }
}
