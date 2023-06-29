using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer;
using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems;
using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager
{
    public class cNeuronLayerManager
    {
        Random m_Random = new Random();
        public List<cNeuronLayer> NeuronLayers { get; set; }
        public cNeuronLayerManager()
        {
            NeuronLayers = new List<cNeuronLayer>();
        }

        public void AddLayer(int _NeuronCount)
        {
            cNeuronLayer __NeuronLayer = new cNeuronLayer(this);
            __NeuronLayer.CreateNeurons(NeuronTypes.NormalNeuron, _NeuronCount);

            if (NeuronLayers.Count > 0)
            {
                cNeuronLayer __LastNeuronLayer = NeuronLayers.Last();
                __LastNeuronLayer.CreateNeurons(NeuronTypes.BiasNeuron, 3);
                __LastNeuronLayer.FullConnectToLayer(__NeuronLayer);
            }

            NeuronLayers.Add(__NeuronLayer);
        }

        public cNeuronLayer GetOutputLayer()
        {
            return NeuronLayers.Last();
        }

        public void SetInputValues(double[] _Inputs)
        {
            cNeuronLayer __NeuronLayer = NeuronLayers.First();
            List<cBaseNeuron> __Neurons = __NeuronLayer.Neurons.Where(__Item => __Item.NeuronType.ID == NeuronTypes.NormalNeuron.ID).ToList();

            if (_Inputs.Length == __Neurons.Count)
            {
                for (int i = 0; i < __Neurons.Count; i++)
                {
                    __NeuronLayer.Neurons[i].Value = _Inputs[i];
                }
            }
            else
            {
                throw new InvalidOperationException("Length of input is not correlate with input neurons in neural network.");
            }
        }

        public void FeedForward()
        {
            cNeuronLayer __NeuronLayer = NeuronLayers.First();
            __NeuronLayer.CalculateNeuronValues();
        }

        public double CalculateError(double[] _Targets)
        {
            cNeuronLayer __OutputLayer = NeuronLayers.Last();
            return __OutputLayer.CalculateNeuronError(_Targets);
        }



        public void BackPropagate(double _LearningRate)
        {
            cNeuronLayer __OutputLayer = NeuronLayers.Last();
            __OutputLayer.BackPropagate(_LearningRate);
        }
    }
}
