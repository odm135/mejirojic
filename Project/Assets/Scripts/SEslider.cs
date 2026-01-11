using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SEslider : MonoBehaviour, IPointerUpHandler
{
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    // スライダーを離した瞬間に呼ばれる
    public void OnPointerUp(PointerEventData eventData)
    {
        FindFirstObjectByType<AudioManager>().PlaySE("connect");
    }
}