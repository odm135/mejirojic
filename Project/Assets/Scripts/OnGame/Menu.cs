using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // 背景エフェクト(ぼかし + 暗くする)
    public GameObject bgEffect;

    // メニュー
    public GameObject menu;
    private Canvas menuCanvas;

    // メニューが表示されているか
    public static bool show;

    private bool escPressed = false;

    void Start()
    {
        show = false;
        
        menu.SetActive(false);
        menuCanvas = GetComponent<Canvas>();
        menuCanvas.sortingOrder = 1;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !escPressed)
        {
            escPressed = true;
            ToggleMenu();
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escPressed = false;
        }
    }

    public void ToggleMenu()
    {
        if (Table.show || Timer.stop && !StageSetting.Tutorial) return;

        show = !show;
        Main.operable = !Main.operable;
        bgEffect.SetActive(show);
        menu.SetActive(show);

        FindFirstObjectByType<AudioManager>().PlaySE("menu");

        if (show)
        {
            menuCanvas.sortingOrder = 3;
        }
        else
        {
            menuCanvas.sortingOrder = 1;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
