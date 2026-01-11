using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Tutorial()
    {
        StageSetting.Tutorial = true;
        StageSetting.stock_AND = 0;
        StageSetting.stock_OR = 0;
        StageSetting.stock_NOT = 0;
        StageSetting.stock_NAND = 0;
        StageSetting.stock_NOR = 0;
        StageSetting.stock_XOR = 0;
        StageSetting.stock_XNOR = 0;

        StageSetting.x00 = false;
        StageSetting.x01 = false;
        StageSetting.x10 = false;
        StageSetting.x11 = true;

        StageSetting.TimeLimit = 999;
        SceneManager.LoadScene("Game");
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Setting()
    {
        SceneManager.LoadScene("Setting");
    }
}
