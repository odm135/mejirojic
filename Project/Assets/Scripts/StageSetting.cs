using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSetting : MonoBehaviour
{
    // それぞれのゲートの使用可能個数
    public static int stock_AND = 99;
    public static int stock_OR = 99;
    public static int stock_NOT = 99;
    public static int stock_NAND = 99;
    public static int stock_NOR = 99;
    public static int stock_XOR = 99;
    public static int stock_XNOR = 99;

    // 解答となる真理値表の値
    public static bool x00 = false;
    public static bool x01 = false;
    public static bool x10 = false;
    public static bool x11 = false;

    // 制限時間
    public static float TimeLimit = 9999;

    public static bool Tutorial = false;

    private bool escPressed = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !escPressed)
        {
            escPressed = true;
            MainMenu();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escPressed = false;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Stage1()
    {
        stock_AND = 1;
        stock_OR = 0;
        stock_NOT = 1;
        stock_NAND = 0;
        stock_NOR = 0;
        stock_XOR = 0;
        stock_XNOR = 0;

        x00 = true;
        x01 = true;
        x10 = true;
        x11 = false;

        Tutorial = false;

        if (!Setting.HelpMode) TimeLimit = 180;
        else TimeLimit = 2000;

        SceneManager.LoadScene("Game");
    }

    public void Stage2()
    {
        stock_AND = 1;
        stock_OR = 1;
        stock_NOT = 1;
        stock_NAND = 0;
        stock_NOR = 0;
        stock_XOR = 0;
        stock_XNOR = 0;

        x00 = true;
        x01 = false;
        x10 = true;
        x11 = true;

        Tutorial = false;

        if (!Setting.HelpMode) TimeLimit = 180;
        else TimeLimit = 2000;

        SceneManager.LoadScene("Game");
    }

    public void Stage3()
    {
        stock_AND = 2;
        stock_OR = 1;
        stock_NOT = 2;
        stock_NAND = 0;
        stock_NOR = 0;
        stock_XOR = 0;
        stock_XNOR = 0;

        x00 = false;
        x01 = true;
        x10 = true;
        x11 = false;

        Tutorial = false;

        if (!Setting.HelpMode) TimeLimit = 240;
        else TimeLimit = 2000;

        SceneManager.LoadScene("Game");
    }

    public void Stage4()
    {
        stock_AND = 1;
        stock_OR = 0;
        stock_NOT = 0;
        stock_NAND = 1;
        stock_NOR = 1;
        stock_XOR = 0;
        stock_XNOR = 0;

        x00 = false;
        x01 = false;
        x10 = false;
        x11 = false;

        Tutorial = false;

        if (!Setting.HelpMode) TimeLimit = 240;
        else TimeLimit = 2000;

        SceneManager.LoadScene("Game");
    }

    public void Stage5()
    {
        stock_AND = 0;
        stock_OR = 0;
        stock_NOT = 0;
        stock_NAND = 4;
        stock_NOR = 0;
        stock_XOR = 0;
        stock_XNOR = 0;

        x00 = true;
        x01 = false;
        x10 = false;
        x11 = false;

        Tutorial = false;

        if (!Setting.HelpMode) TimeLimit = 240;
        else TimeLimit = 2000;

        SceneManager.LoadScene("Game");
    }

    public void Stage6()
    {
        stock_AND = 1;
        stock_OR = 0;
        stock_NOT = 1;
        stock_NAND = 0;
        stock_NOR = 0;
        stock_XOR = 1;
        stock_XNOR = 0;

        x00 = false;
        x01 = false;
        x10 = true;
        x11 = false;

        Tutorial = false;

        if (!Setting.HelpMode) TimeLimit = 300;
        else TimeLimit = 2000;

        SceneManager.LoadScene("Game");
    }
    
        public void Stage7()
    {
        stock_AND = 0;
        stock_OR = 1;
        stock_NOT = 1;
        stock_NAND = 0;
        stock_NOR = 0;
        stock_XOR = 0;
        stock_XNOR = 1;

        x00 = false;
        x01 = false;
        x10 = true;
        x11 = false;

        Tutorial = false;

        if (!Setting.HelpMode) TimeLimit = 300;
        else TimeLimit = 2000;

        SceneManager.LoadScene("Game");
    }
}
