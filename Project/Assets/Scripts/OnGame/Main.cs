using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Main : MonoBehaviour
// 全体共通変数を管理するコード
{
    // メインカメラ
    public static Camera cam;

    // マウスの座標
    public static Vector3 mousePos;

    // 操作（ドラッグ等）可能か否か
    public static bool operable;

    // スクロールエリア
    public ScrollRect sr;

    // ぼかし背景
    public GameObject bgEffect;

    // 真理値表
    public GameObject table;
    public Canvas tableCanvas;

    // タイマーのスクリプト
    public Timer timer;

    public Text centerText;
    public Tutorial tut;


    void Awake()
    {
        Application.targetFrameRate = 120;  // フレームレートを120に設定
        cam = Camera.main;
    }

    void Start()
    {
        if (!StageSetting.Tutorial) StartCoroutine(GameStart());
        else centerText.text = "";
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
    }

    private IEnumerator GameStart()
    {
        operable = false;
        sr.vertical = false;
        tableCanvas.sortingOrder = 3;
        bgEffect.SetActive(true);
        table.SetActive(true);

        for (int i = 5; i > 0; i--)
        {
            centerText.text = "この真理値表を満たすように回路を組もう！ " + i;
            yield return new WaitForSeconds(1f);
        }

        centerText.text = "";
        operable = true;
        sr.vertical = true;
        tableCanvas.sortingOrder = 1;
        bgEffect.SetActive(false);
        table.SetActive(false);

        timer.StartTimer();
    }
}
