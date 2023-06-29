using MyNeuralNetwork.nAI.nNeuronLayerManager;
using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static cNeuronLayerManager NeuronLayerManager { get; set; }
    static void Main()
    {
        NeuronLayerManager = new cNeuronLayerManager();
        CreateLayers();
        Train();

        Random __Random = new Random();

        int __True = 0;
        int __False = 0;

        for (int i = 0; i < 10000; i++)
        {
            double __X = __Random.Next(500);
            double __Y = __Random.Next(500);

            double __Distance = GetDistance(__X, __Y, 250f, 250f);

            var inputs = new[] { __X / 500 , __Y / 500 };

            NeuronLayerManager.SetInputValues(inputs);
            NeuronLayerManager.FeedForward();
            cNeuronLayer __NeuronLayer = NeuronLayerManager.GetOutputLayer();
            double __PredictDistance = __NeuronLayer.Neurons.First().Value;
            double __PredictIn = __NeuronLayer.Neurons.Last().Value;
            var absoluteResponse = __PredictIn > 0.5 ? 1 : 0;

            Console.WriteLine($"RealDistance: {__Distance} : PredictDistance: {__PredictDistance * 500} , In Circle : { absoluteResponse}");

           /* if (absoluteResponse < 0.5)
            {
                Console.WriteLine("aa");
            }
            else
            {
                Console.WriteLine("bb");
            }
            */
            if ((__Distance <= 200 && absoluteResponse > 0.5) ||
                __Distance > 200 && absoluteResponse < 0.5)
            {
                __True++;
            }
            else
            {
                __False++;
            }
        }

        Console.WriteLine($"True: {__True}");
        Console.WriteLine($"False: {__False}");
    }

    static void Train()
    {
        for (var __EpochIndex = 0; __EpochIndex < 500; __EpochIndex++)
        {
            List<cTrainingData<double[], double[]>> __TrainingDataSet = GetTrainingData();

            List<double> __Errors = new List<double>();
            foreach (var __TrainingData in __TrainingDataSet)
            {
                NeuronLayerManager.SetInputValues(__TrainingData.Data);
                NeuronLayerManager.FeedForward();
                double __Error = NeuronLayerManager.CalculateError( __TrainingData.Targets );
                __Errors.Add(__Error);
                NeuronLayerManager.BackPropagate(0.1);
            }

            var __AverageError = __Errors.Sum() / __Errors.Count;
            Console.WriteLine(__AverageError);
        }
    }



    static double GetDistance(double _X1, double _Y1, double _X2, double _Y2)
    {
        double distance = (float)Math.Sqrt((Math.Pow(_X1 - _X2, 2) + Math.Pow(_Y1 - _Y2, 2)));
        return distance;
    }

    static List<cTrainingData<double[], double[]>> GetTrainingData()
    {
        List<cTrainingData<double[], double[]>> __TrainingDataSet = new List<cTrainingData<double[], double[]>>();
        Random __Random = new Random();

        for (int __X = 0; __X < 500; __X += 10)
        {
            for (int __Y = 0; __Y < 500; __Y += 10)
            {
                //int __NewX = __Random.Next(500);
                //int __NewY = __Random.Next(500);
                double __Distance = GetDistance((double)__X, (double)__Y, 250d, 250d);

                double __Target = __Distance <= 200 ? 0.9999f : 0.0001f;

                var __TrainingData = new cTrainingData<double[], double[]>
                {
                    Data = new[] { (double)__X / 500, (double)__Y / 500 },
                    Targets = new[] { __Distance / 500, __Target }
                };

                __TrainingDataSet.Add(__TrainingData);
            }
        }

        return __TrainingDataSet;
    }


    public static void CreateLayers()
    {
        NeuronLayerManager.AddLayer(2);
        NeuronLayerManager.AddLayer(6);
        NeuronLayerManager.AddLayer(6);
        NeuronLayerManager.AddLayer(6);
        //NeuronLayerManager.AddLayer(4, false);
        //NeuronLayerManager.AddLayer(12, true);
        NeuronLayerManager.AddLayer(2);
    }
}