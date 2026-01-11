using UnityEngine;

public class WireDragging : MonoBehaviour
// ドラッグ中のワイヤーを描画するコード
{
    private LineRenderer line;

    public Material mat;
    public float width = 0.05f;

    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();

        line.material = mat;
        line.startWidth = width;
        line.endWidth = width;
        line.positionCount = 0;
    }

    public void StartLine(Vector3 startPos)
    {
        startPos.z = 0;
        line.positionCount = 2;
        line.SetPosition(0, startPos);
    }

    public void EndLine()
    {
        line.positionCount = 0;
    }

    void Update()
    {
        if (line.positionCount == 2)
        {
            line.SetPosition(1, Main.mousePos);
        }
    }
}
