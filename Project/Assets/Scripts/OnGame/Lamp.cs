using UnityEngine;
using UnityEngine.UI;

public class Lamp : MonoBehaviour
{
    public bool power;
    private In input;

    public Image img;
    public Sprite onImg;
    public Sprite offImg;

    void Start()
    {
        input = GetComponentInChildren<In>();
        power = input.power;

        img.sprite = power ? onImg : offImg;
    }

    void Update()
    {
        if (power != input.power)
        {
            power = input.power;
            if (!Judge.work) img.sprite = power ? onImg : offImg;
        }
    }
}
