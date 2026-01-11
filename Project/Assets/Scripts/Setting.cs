using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public Slider BGM_slider;
    public Slider SE_slider;

    public AudioManager audioManager;

    public static bool HelpMode = false;  // お助けモード
    public Toggle HelpToggle;

    private bool escPressed = false;


    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();

        // スライダーの値が変わったら volumeChange を呼ぶ
        BGM_slider.onValueChanged.AddListener(BGMvolumeChange);
        SE_slider.onValueChanged.AddListener(SEvolumeChange);

        // 設定画面を開いたときにスライダーの値を現在の音量に合わせる
        BGM_slider.value = AudioManager.bgmVol / 0.3f;
        SE_slider.value = audioManager.seSource.volume;

        // お助けモードのチェックボックス同期
        HelpToggle.isOn = HelpMode;
    }

    void BGMvolumeChange(float v)
    {
        // bgmSource の音量をスライダーの値で更新
        AudioManager.bgmVol = v * 0.3f;
        audioManager.bgmSource.volume = AudioManager.bgmVol;
    }
    void SEvolumeChange(float v)
    {
        // seSource の音量をスライダーの値で更新
        audioManager.seSource.volume = v;
    }

    public void ToggleHelpMode()
    {
        FindFirstObjectByType<AudioManager>().PlaySE("connect");
        HelpMode = HelpToggle.isOn;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !escPressed)
        {
            escPressed = true;
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escPressed = false;
        }
    }


    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
