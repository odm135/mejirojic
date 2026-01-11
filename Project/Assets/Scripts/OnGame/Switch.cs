using UnityEngine;
using UnityEngine.UI;

public class Switch : GateBase
{

    public bool power;
    private Out output;

    public Image img;
    public Sprite onImg;
    public Sprite offImg;

    public override bool GetOutput()
    {
        return power;
    }

    void Start()
    {
        power = true;
        output = GetComponentInChildren<Out>();
        output.power = power;

        img.sprite = onImg;
    }

    void OnMouseDown()
    {
        if (Main.operable)
        {
            power = !power;

            img.sprite = power ? onImg : offImg;

            FindFirstObjectByType<AudioManager>().PlaySE("switch");
        }
    }

    void Update()
    {
        output.power = power;
    }

}
