using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Judge : MonoBehaviour
{

    private bool result;

    public Switch swA;
    public Switch swB;
    public Lamp lamp;

    public GameObject bgEffect;  // 背景エフェクト(ぼかし + 暗くする)
    public GameObject correctImg;  // 正解時に表示するオブジェクト
    public GameObject timeUpImg;  // 正解時に表示するオブジェクト
    public GameObject endMenu;

    public Text timerText;
    public Text centerText;
    public Text errText;
    private Coroutine textCoroutine;

    public static bool work = false;

    void Start()
    {
        bgEffect.SetActive(false);
        correctImg.SetActive(false);
        timeUpImg.SetActive(false);
        endMenu.SetActive(false);
        errText.text = "";
    }

    // 正誤判定するコード
    public void Judgement()
    {
        if (Main.operable)
        {
            result = true;
            if (textCoroutine != null) StopCoroutine(textCoroutine);
            errText.text = "ジャッジ中...";
            work = true;

            // ストックが余っているゲートがあったら不正解
            GateSamples[] samples = FindObjectsByType<GateSamples>(FindObjectsSortMode.None);
            for (int i = 0; i < samples.Length; i++)
            {
                if (samples[i].stock != 0)
                {
                    result = false;
                    showText("ストックが残っています");
                    FindFirstObjectByType<AudioManager>().PlaySE("caution");
                    work = false;
                    break;
                }
            }

            if (result)
            {
                // 配置済みゲートの In が未接続なら不正解
                In[] ins = FindObjectsByType<In>(FindObjectsSortMode.None);
                for (int i = 0; i < ins.Length; i++)
                {
                    if (!ins[i].isConnected)
                    {
                        result = false;
                        showText("接続されていない端子があります");
                        FindFirstObjectByType<AudioManager>().PlaySE("caution");
                        work = false;
                        break;
                    }
                }
            }

            if (result)
            {
                // 配置済みゲートの Out が未接続なら不正解
                Out[] outs = FindObjectsByType<Out>(FindObjectsSortMode.None);
                for (int i = 0; i < outs.Length; i++)
                {
                    if (outs[i].isConnected == 0)
                    {
                        result = false;
                        showText("接続されていない端子があります");
                        FindFirstObjectByType<AudioManager>().PlaySE("caution");
                        work = false;
                        break;
                    }
                }
            }

            if (result) StartCoroutine(J());
        }
        
        IEnumerator J()
        {
            Main.operable = false;

            bool oriA = swA.power;
            bool oriB = swB.power;

            int gateCount = FindObjectsByType<GateBase>(FindObjectsSortMode.None).Length;
            int wireCount = FindObjectsByType<WireFixed>(FindObjectsSortMode.None).Length;
            int waitFrames = gateCount * 2 + wireCount + 1;

            swA.power = false;
            swB.power = false;
            for (int i = 0; i < waitFrames; i++) yield return null;
            if (lamp.power != StageSetting.x00) result = false;

            swA.power = false;
            swB.power = true;
            for (int i = 0; i < waitFrames; i++) yield return null;
            if (lamp.power != StageSetting.x01) result = false;

            swA.power = true;
            swB.power = false;
            for (int i = 0; i < waitFrames; i++) yield return null;
            if (lamp.power != StageSetting.x10) result = false;

            swA.power = true;
            swB.power = true;
            for (int i = 0; i < waitFrames; i++) yield return null;
            if (lamp.power != StageSetting.x11) result = false;

            swA.power = oriA;  // 元の入力状態に戻す
            swB.power = oriB;

            if (result)
            {
                // 正解だった場合の処理
                bgEffect.SetActive(true);
                correctImg.SetActive(true);
                endMenu.SetActive(true);
                Timer.stop = true;

                errText.text = "";
                timerText.text = "";
                if (!StageSetting.Tutorial) centerText.text = "Clear Time: " + Timer.passed.ToString("f1") + "s";
                else Tutorial.end = true;
                work = false;

                FindFirstObjectByType<AudioManager>().PlaySE("correct");
            }
            else
            {
                // 不正解だった場合の処理
                showText("不正解");
                Main.operable = true;
                work = false;

                FindFirstObjectByType<AudioManager>().PlaySE("incorrect");
            }
        }
    }

    public void showText(string s)
    {
        // すでに動いてるコルーチンがあれば止める
        if (textCoroutine != null) StopCoroutine(textCoroutine);

        // 新しいメッセージを開始
        textCoroutine = StartCoroutine(showTextCor(s));
    }

    private IEnumerator showTextCor(string s)
    {
        errText.text = s;
        yield return new WaitForSeconds(5f);
        errText.text = "";
        textCoroutine = null;  // 終了したら参照をクリア
    }

    public void TimeOver()
    {
        errText.text = "";
        timerText.text = "";
        centerText.text = "タイムオーバー";
        FindFirstObjectByType<AudioManager>().PlaySE("incorrect");

        timeUpImg.SetActive(true);
        bgEffect.SetActive(true);
        endMenu.SetActive(true);
    }
}