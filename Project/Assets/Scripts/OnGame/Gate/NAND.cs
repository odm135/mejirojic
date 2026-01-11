using UnityEngine;

public class NAND : GateBase
{
    public In inputA;
    public In inputB;
    public Out output;

    void Update()
    {
        if (inputA != null && inputB != null)
        {
            output.power = !(inputA.power && inputB.power);
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
