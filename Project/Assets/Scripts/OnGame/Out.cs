using UnityEngine;

public class Out : MonoBehaviour
{
    public bool power;
    public int isConnected;

    // 出力元
    private GateBase parentGate;

    private WireDragging wd;

    private RectTransform rt;


    void Start()
    {
        parentGate = transform.parent.GetComponent<GateBase>();

        wd = FindFirstObjectByType<WireDragging>();

        rt = GetComponent<RectTransform>();

        isConnected = 0;
    }

    void OnMouseDown()
    {
        if (!Table.show && !Menu.show)
        {
            wd.StartLine(transform.position);
        }
    }

    void OnMouseUp()
    {
        // 線で結ぶための処理
        // マウスの位置にInputConnectorがあるか調べる
        Collider2D hit = Physics2D.OverlapPoint(new Vector2(Main.mousePos.x, Main.mousePos.y));

        if (hit != null)
        {
            In input = hit.GetComponent<In>();
            if (input != null && !input.isConnected)
            {
                // 接続時のSEを鳴らす
                FindFirstObjectByType<AudioManager>().PlaySE("connect");

                GateBase inputGate = input.GetComponentInParent<GateBase>();
                if (inputGate == parentGate)
                {
                    // 接続せずに終了（ドラッグ中の線があれば消す）
                    if (wd != null) wd.EndLine();
                    return;
                }

                // ワイヤー用のオブジェクト生成
                GameObject wireObj = new GameObject("Conneected");
                wireObj.transform.SetParent(GameObject.Find("Wire").transform);
                wireObj.transform.localPosition = Vector3.zero;

                // ワイヤー生成
                LineRenderer line = wireObj.AddComponent<LineRenderer>();
                line.material = wd.mat;
                line.positionCount = 2;
                line.startWidth = wd.width;
                line.endWidth = wd.width;

                // WireFixed.csをアタッチ
                var wf = wireObj.AddComponent<WireFixed>();
                wf.from = this;
                wf.to = input;

                // 接続済みにする
                input.isConnected = true;
                isConnected++;
            }
        }
        wd.EndLine();
    }

    void OnMouseEnter()
    {
        if (Main.operable) rt.sizeDelta = rt.sizeDelta * 1.2f;
    }

    void OnMouseExit()
    {
        if (Main.operable) rt.sizeDelta = rt.sizeDelta / 1.2f;
    }


    void Update()
    {
        /* 入出力制御 */
        if (parentGate != null)
        {
            power = parentGate.GetOutput();
        }
    }
}
