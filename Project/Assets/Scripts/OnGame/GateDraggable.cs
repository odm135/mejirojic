using UnityEngine;

public class GateDraggable : MonoBehaviour
// ドラッグで移動できるゲートのコード
{
    // 変数
    private bool isDragging; // ドラッグ中かどうか

    private Vector3 offset;  // クリックした座標とゲートの座標の差

    public GateSamples gateSamples;  // 元のGateSamplesスクリプト

    // 動かせる範囲
    private float minX = -7.9f;
    private float maxX = 7.9f;
    private float minY = -4.5f;
    private float maxY = 3.43f;

    // 削除される範囲
    private float deleteX = -4.6f;

    RectTransform rt;

    void Start()
    {
        isDragging = true;

        rt = GetComponent<RectTransform>();
        Vector3 pos = rt.anchoredPosition3D;
        pos.z = 0f;
        rt.anchoredPosition3D = pos;
    }

    void OnMouseDown()
    {
        isDragging = true;

        // クリックした座標とゲートの座標の差を取得
        offset = transform.position - Main.mousePos;
    }


    void Update()
    {
        if (!Input.GetMouseButton(0) && isDragging)
        {
            isDragging = false;

            // 一定範囲外に出たら削除
            if (Main.mousePos.x < deleteX)
            {
                Destroy(this.gameObject);
                gateSamples.stock++;
                gateSamples.stockLabel.text = "×" + gateSamples.stock;
                FindFirstObjectByType<AudioManager>().PlaySE("gate");
            }
        }

        if (isDragging && Main.operable)
        {
            // offsetを加えた位置
            Vector3 newPos = Main.mousePos + offset;

            // 範囲を制限
            newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

            transform.position = newPos;
        }
    }
}
