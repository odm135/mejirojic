using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgmSource;
    public AudioSource seSource;

    public AudioClip commonBGM;  // ゲーム中以外に流れるBGM
    public AudioClip gameBGM;    // ゲーム中に流れるBGM (若干音が大きい)

    public static float bgmVol;

    [System.Serializable]
    public struct SEItem
    {
        public string name;      // 呼び出し名
        public AudioClip clip;   // 音声ファイル
    }
    public SEItem[] seList;

    void Awake()
    {
        // すでにAudioManagerがある場合は消す
        AudioManager[] managers = FindObjectsByType<AudioManager>(FindObjectsSortMode.None);
        if (managers.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            bgmVol = 0.3f;
            bgmSource.volume = 0.3f;
            seSource.volume = 1f;
        }

        DontDestroyOnLoad(gameObject);

        // シーン切り替え時に呼ばれる関数を登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // シーンが変わったときに実行される
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ゲームシーンならゲームBGM
        if (scene.name == "Game")
        {
            ChangeBGM(gameBGM, 0.7f);
        }
        else
        {
            ChangeBGM(commonBGM, 1f);
        }
    }

    // BGMを切り替える関数
    private void ChangeBGM(AudioClip newClip, float v)
    {
        if (bgmSource.clip == newClip) return;

        bgmSource.volume = bgmVol * v;

        bgmSource.clip = newClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // SEを鳴らす関数
    public void PlaySE(string seName)
    {
        foreach (var se in seList)
        {
            if (se.name == seName)
            {
                seSource.PlayOneShot(se.clip, 2f);
                return;
            }
        }
    }
}
