using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rt;
    private Vector3 originalScale;
    public float scaleFactor = 1.1f; // ホバー時に拡大する倍率

    void Start()
    {
        rt = GetComponent<RectTransform>();
        originalScale = rt.localScale;
    }

    // マウスがボタンに乗ったとき
    public void OnPointerEnter(PointerEventData eventData)
    {
        rt.localScale = originalScale * scaleFactor;
    }

    // マウスが離れたとき
    public void OnPointerExit(PointerEventData eventData)
    {
        rt.localScale = originalScale;
    }
}