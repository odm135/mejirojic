using UnityEngine;

public class In : MonoBehaviour
{
    public bool power;
    public bool isConnected;

    private RectTransform rt;

    void Start()
    {
        power = false;
        isConnected = false;

        rt = GetComponent<RectTransform>();
    }

    void OnMouseEnter()
    {
        if (Main.operable) rt.sizeDelta = rt.sizeDelta * 1.2f;
    }

    void OnMouseExit()
    {
        if (Main.operable) rt.sizeDelta = rt.sizeDelta / 1.2f;
    }

}
