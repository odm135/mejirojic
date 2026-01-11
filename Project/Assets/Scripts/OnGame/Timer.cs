using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    private float start;
    private float remain;
    public static float passed;

    public static bool stop = false;

    public Judge judge;

    void Start()
    {
        stop = true;
        timerText.text = "";
    }

    public void StartTimer()
    {
        start = Time.time;
        remain = StageSetting.TimeLimit;
        stop = false;
    }

    void Update()
    {
        if (stop) return;

        passed = Time.time - start;
        remain = StageSetting.TimeLimit - passed;
        timerText.text = "TIME: " + remain.ToString("f1") + "s";

        if (remain <= 0)
        {
            judge.TimeOver();
            stop = true;
        }
    }
}
