using System;

public static class MathHelper
{
    public static double Sigmoid(double _Input)
    {
        double __Response = 1.0f / (1.0f + Math.Exp(-_Input));
        return __Response;
    }

    public static double SigmoidFunction(double _Input)
    {
        if (_Input < -45.0)
        {
            return 0.0;
        }
        else if (_Input > 45.0)
        {
            return 1.0;
        }
        return 1.0 / (1.0 + Math.Exp(-_Input));

    }

    public static double IdentityFunction(double x)
    {
        return x;
    }

    public static double SigmoidDerivative(double _Input)
    {
        return _Input * (1 - _Input);
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

