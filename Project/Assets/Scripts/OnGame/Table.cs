using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    // 背景エフェクト(ぼかし + 暗くする)
    public GameObject bgEffect;

    // 真理値表
    public GameObject table;
    private Canvas tableCanvas;

    // 真理値表が表示されているか
    public static bool show;

    public Image Img_x00;
    public Image Img_x01;
    public Image Img_x10;
    public Image Img_x11;
    
    public Sprite x0;
    public Sprite x1;

    private bool tabPressed = false;

    void Start()
    {
        show = false;

        tableCanvas = GetComponent<Canvas>();
        tableCanvas.sortingOrder = 1;

        Img_x00.sprite = StageSetting.x00 ? x1 : x0;
        Img_x01.sprite = StageSetting.x01 ? x1 : x0;
        Img_x10.sprite = StageSetting.x10 ? x1 : x0;
        Img_x11.sprite = StageSetting.x11 ? x1 : x0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !tabPressed)
        {
            tabPressed = true;
            ToggleTruthTable();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            tabPressed = false;
        }
    }

    public void ToggleTruthTable()
    {
        if (Menu.show || Timer.stop && !StageSetting.Tutorial) return;

        show = !show;
        Main.operable = !Main.operable;
        bgEffect.SetActive(show);
        table.SetActive(show);

        FindFirstObjectByType<AudioManager>().PlaySE("menu");

        if (show)
        {
            tableCanvas.sortingOrder = 3;
        }
        else
        {
            tableCanvas.sortingOrder = 1;
        }
    }
}
