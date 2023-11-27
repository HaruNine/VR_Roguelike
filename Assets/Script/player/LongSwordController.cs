using UnityEngine;
using OculusSampleFramework;

public class LongSwordController : MonoBehaviour
{
    private Collider longSwordCollider;
    private Renderer longSwordRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // "LongSword" 오브젝트의 Collider와 Renderer 가져오기
        longSwordCollider = GetComponent<Collider>();
        longSwordRenderer = GetComponent<Renderer>();
    }
    public void SetVisibilityAndCollider(bool isVisible)
    {
        // Collider 활성화 여부 설정
        longSwordCollider.enabled = isVisible;

        // 투명도 조절
        Color currentColor = longSwordRenderer.material.color;
        float targetAlpha = isVisible ? 1.0f : 0.0f;

        // 새로운 색상 설정
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha);
        longSwordRenderer.material.color = newColor;
    }
}
