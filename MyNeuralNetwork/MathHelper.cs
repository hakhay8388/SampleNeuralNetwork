using System;

public static class MathHelper
{
    public static double Sigmoid(double _Input)
    {
        double __Response = 1.0f / (1.0f + Math.Exp(-_Input));
        return __Response;
    }

    /*public static double Sigmoid(double _Input)
    {
        double __Value = Math.Exp(_Input);
        return __Value / (1.0f + __Value);
    }
    */
    public static int GetIndexOfMaxValue(double[] _Values)
    {
        int __AnswerIndex = 0;
        double __Max = 0;

        for (var __NeuronIndex = 0; __NeuronIndex < _Values.Length; __NeuronIndex++)
        {
            if (_Values[__NeuronIndex] > __Max)
            {
                __Max = _Values[__NeuronIndex];
                __AnswerIndex = __NeuronIndex;
            }
        }

        return __AnswerIndex;
    }
}

