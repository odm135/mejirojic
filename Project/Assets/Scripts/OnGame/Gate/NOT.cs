using UnityEngine;

public class NOT : GateBase
{
    public In input;
    public Out output;

    void Update()
    {
        if (input != null)
        {
            output.power = !input.power;
        }
        else
        {
            output.power = false;
        }
    }

    public override bool GetOutput()
    {
        return output.power;
    }
}
