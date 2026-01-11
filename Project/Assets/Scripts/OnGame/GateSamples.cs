using UnityEngine;
using UnityEngine.UI;

public class GateSamples : MonoBehaviour
// サンプルゲートを複製するコード
{
    // 変数
    public GameObject cloneGate;
    public Transform DragCanvas;


    public int stock;
    public Text stockLabel;

    public ScrollRect sr;


    void Start()
    {
        if (cloneGate.name == "AND")
        {
            stock = StageSetting.stock_AND;
        }
        else if (cloneGate.name == "OR")
        {
            stock = StageSetting.stock_OR;
        }
        else if (cloneGate.name == "NOT")
        {
            stock = StageSetting.stock_NOT;
        }
        else if (cloneGate.name == "NAND")
        {
            stock = StageSetting.stock_NAND;
        }
        else if (cloneGate.name == "NOR")
        {
            stock = StageSetting.stock_NOR;
        }
        else if (cloneGate.name == "XOR")
        {
            stock = StageSetting.stock_XOR;
        }
        else if (cloneGate.name == "XNOR")
        {
            stock = StageSetting.stock_XNOR;
        }
        stockLabel.text = "×" + stock;

    }


    // マウスボタンが押された瞬間に呼び出される
    void OnMouseDown()
    {
        if (stock > 0 && Main.operable)
        {
            GameObject newGate = Instantiate(cloneGate, DragCanvas);
            newGate.GetComponent<GateDraggable>().gateSamples = this;
            FindFirstObjectByType<AudioManager>().PlaySE("gate");
            
            stock--;
            stockLabel.text = "×" + stock;

            sr.enabled = false; // スクロールを無効化
        }
    }

    void OnMouseUp()
    {
        sr.enabled = true; // スクロールを有効化
    }
}
