using UnityEngine;
using System.Collections.Generic;

public class WireFixed : MonoBehaviour
// 固定されたワイヤーを描画し、信号を伝達するコード
{
    public Out from;
    public In to;

    private LineRenderer line;

    // コライダー
    private EdgeCollider2D edge;

    // 入出力端子の座標
    public Vector3 startPos;
    public Vector3 endPos;

    void Start()
    {
        // 線を描画するためのコンポーネントを追加
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;

        startPos = from.transform.position;
        endPos = to.transform.position;
        startPos.z = 0;
        endPos.z = 0;

        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);

        to.power = from.power;

        // EdgeCollider2Dの設定
        edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.edgeRadius = 0.1f; // 当たり判定の太さ設定

        Vector2 p0 = new Vector2(startPos.x, startPos.y);
        Vector2 p1 = new Vector2(endPos.x, endPos.y);
        edge.SetPoints(new List<Vector2> { p0, p1 });
    }

    void Update()
    {
        // 入出力端子が設定されていなければ削除
        if (from == null || to == null)
        {
            if (to != null)
            {
                to.power = false;
                to.isConnected = false;
            }
            Destroy(gameObject);
            return;
        }

        // 線を更新
        startPos = from.transform.position;
        endPos = to.transform.position;

        startPos.z = 0;
        endPos.z = 0;

        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);

        Vector2 p0 = new Vector2(startPos.x, startPos.y);
        Vector2 p1 = new Vector2(endPos.x, endPos.y);
        edge.SetPoints(new List<Vector2> { p0, p1 });

        // 電源を更新
        to.power = from.power;
    }
    
    // マウスがワイヤー上にあるとき右クリックしているとワイヤー削除
    void OnMouseOver()
    {
        if (Input.GetMouseButton(1) && Main.operable) 
        {
            to.power = false;
            to.isConnected = false;
            from.isConnected -= 1;
            FindFirstObjectByType<AudioManager>().PlaySE("disconnect");
            Destroy(gameObject);
        }
    }
}
