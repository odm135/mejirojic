using System;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GateSamples ANDsample;
    public Text ANDstock;
    public GameObject ttBtn;  // 真理値表ボタン
    public GameObject judgeBtn;  // ジャッジボタン
    public Switch swA;
    public Switch swB;
    public Text tutText;

    public GameObject mouseLeftImg;
    private RectTransform MLrt;

    public GameObject guideImg;
    private RectTransform Grt;

    private Vector2 guideStart;
    private Vector2 guideEnd;
    private float guideElapsed = 0f;
    private float guideDuration = 2f;


    public static int step = 0;
    public static bool end;

    public GameObject TutObj;

    private bool preA = true;
    private bool preB = true;
    private bool flag = false;

    void Start()
    {
        TutObj.SetActive(StageSetting.Tutorial);
        if (!StageSetting.Tutorial)
        {
            tutText.text = "";
            return;
        }

        step = 0;
        end = false;
        Main.operable = true;

        MLrt = mouseLeftImg.GetComponent<RectTransform>();
        MLrt.anchoredPosition = new Vector2(270f, 455f);

        Grt = guideImg.GetComponent<RectTransform>();

        ttBtn.SetActive(false);
        judgeBtn.SetActive(false);
        guideImg.SetActive(false);
        tutText.text = "めじろじっくへようこそ！";
    }

    void Update()
    {
        if (!StageSetting.Tutorial) return;
        if (Menu.show)
        {
            TutObj.SetActive(false);
            return;
        }
        else
        {
            TutObj.SetActive(true);
        }

        int countGate = FindObjectsByType<GateDraggable>(FindObjectsSortMode.None).Length;
        int countWire = FindObjectsByType<WireFixed>(FindObjectsSortMode.None).Length;

        switch (step)
        {
            case 0:
                if (Input.GetMouseButtonDown(0))
                {
                    tutText.text = "めじろじっくは、真理値表どおりの入出力になるように\nゲートを配置してワイヤーで接続するパズルゲームです。";
                    MLrt.anchoredPosition = new Vector2(540f, 455f);
                    step = 1;
                }
                break;

            case 1:
                if (Input.GetMouseButtonDown(0))
                {
                    tutText.text = "マウスの左ボタンでゲートをクリック＆ドラッグして、\n右のフィールドに配置することができます。";
                    mouseLeftImg.SetActive(false);

                    ANDsample.stock = 1;
                    ANDstock.text = "×" + ANDsample.stock;

                    guideImg.SetActive(true);
                    guideStart = new Vector2(-750f, 250f);
                    guideEnd = Vector2.zero;
                    guideElapsed = 0f;

                    step = 2;
                }
                break;

            case 2:
                if (countGate == 1 && !Input.GetMouseButton(0))
                {
                    tutText.text = "ゲートを左のエリアに戻すと、ゲートを削除することができます。";

                    GateDraggable[] gate = FindObjectsByType<GateDraggable>(FindObjectsSortMode.None);
                    RectTransform gatert = gate[0].GetComponent<RectTransform>();
                    Vector2 gatePos = gatert.anchoredPosition;
                    guideStart = gatePos;
                    gatePos.x = -750f;
                    guideEnd = gatePos;
                    guideElapsed = 0f;

                    step = 3;
                }
                break;

            case 3:
                if (countGate == 0)
                {
                    tutText.text = "出力端子からクリック＆ドラッグして、入力端子の上で左クリックを離すことで、\n端子同士をつなぐワイヤーを設置することができます。";

                    guideStart = new Vector2(-327f, 170f);
                    guideEnd = new Vector2(839f, -57f);
                    guideElapsed = 0f;

                    step = 4;
                }
                break;

            case 4:
                if (countWire == 1)
                {
                    tutText.text = "スイッチを左クリックすることで、ON/OFFを切り替えることができます。";
                    guideImg.SetActive(false);
                    mouseLeftImg.SetActive(true);
                    MLrt.anchoredPosition = new Vector2(-385f, 105f);

                    preA = swA.power;
                    preB = swB.power;
                    step = 5;
                }
                break;

            case 5:
                if (preA != swA.power || preB != swB.power)
                {
                    tutText.text = "右クリックを押したままワイヤーの上をドラッグすると削除できます。";
                    mouseLeftImg.SetActive(false);
                    if (countWire == 0) flag = false;
                    else
                    {
                        flag = true;

                        WireFixed[] wire = FindObjectsByType<WireFixed>(FindObjectsSortMode.None);
                        Vector3 startPos = wire[0].startPos;
                        Vector3 endPos = wire[0].endPos;
                        Vector3 centerPos = (startPos + endPos) / 2f;
                        Vector3 screenPos = Main.cam.WorldToScreenPoint(centerPos);

                        Canvas canvas = guideImg.GetComponentInParent<Canvas>();
                        RectTransform canvasRt = canvas.GetComponent<RectTransform>();
                        Vector2 uiPos;

                        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                            canvasRt,
                            screenPos,
                            Main.cam,
                            out uiPos
                        );

                        guideImg.SetActive(true);
                        guideStart = uiPos + new Vector2(50f, 50f);
                        guideEnd   = uiPos + new Vector2(-50f, -50f);
                        guideElapsed = 0f;
                    }
                    step = 6;
                }
                break;

            case 6:
                if (countWire == 0 && flag)
                {
                    tutText.text = "右上のボタンをクリックすると、問題の真理値表が表示されます。";
                    ttBtn.SetActive(true);
                    guideImg.SetActive(false);
                    mouseLeftImg.SetActive(true);
                    MLrt.anchoredPosition = new Vector2(857f, 440f);
                    step = 7;
                }
                else if (!flag)
                {
                    if (countWire >= 1) flag = true;
                }
                break;

            case 7:
                if (Table.show)
                {
                    tutText.text = "もう一度押すことで非表示にすることができます。";
                    step = 8;
                }
                break;

            case 8:
                if (!Table.show)
                {
                    tutText.text = "では、この真理値表に従ってANDゲートを配置して、ワイヤーでつなげてみましょう。\n完成したら右下のジャッジボタンをクリック！";
                    mouseLeftImg.SetActive(false);
                    judgeBtn.SetActive(true);
                    step = 9;
                }
                break;

            case 9:
                if (end)
                {
                    tutText.text = "これで操作方法は以上です！\n各ゲートの役割は配布冊子やホームページをご覧ください。";
                    step = 10;
                }
                break;

            default:
                break;
        }

        if (step >= 2)
        {
            guideElapsed += Time.deltaTime;
            float u = Mathf.Clamp01(guideElapsed / guideDuration);
            float s = u * u * (3f - 2f * u);

            Grt.anchoredPosition = Vector2.LerpUnclamped(guideStart, guideEnd, s);
            if (guideElapsed >= guideDuration + 0.5f)
            {
                guideElapsed = 0f;
                Grt.anchoredPosition = guideStart;
            }
        }
    }
}
