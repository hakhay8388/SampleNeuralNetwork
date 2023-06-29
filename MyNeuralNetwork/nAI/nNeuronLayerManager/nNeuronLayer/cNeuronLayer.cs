using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems;
using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer
{
    public class cNeuronLayer
    {
        public cNeuronLayerManager NeuronLayerManager { get; set; }
        public List<cBaseNeuron> Neurons { get; set; }
        public cNeuronLayer(cNeuronLayerManager _NeuronLayerManager)
        {
            Neurons = new List<cBaseNeuron>();
            NeuronLayerManager = _NeuronLayerManager;
        }

        public cNeuronLayer NextLayer
        {
            get
            {
                int __Index = NeuronLayerManager.NeuronLayers.FindIndex(__Item => __Item == this);
                if (NeuronLayerManager.NeuronLayers.Count > __Index + 1)
                {
                    return NeuronLayerManager.NeuronLayers[__Index + 1];
                }
                return null;
            }
        }

        public cNeuronLayer PreviousLayer
        {
            get
            {
                int __Index = NeuronLayerManager.NeuronLayers.FindIndex(__Item => __Item == this);
                if (__Index > 0)
                {
                    return NeuronLayerManager.NeuronLayers[__Index - 1];
                }
                return null;
            }
        }

        public void CreateNeurons(NeuronTypes _NeuronTypes, int _Count)
        {
            Random m_Random = new Random();
            for (int i = 0; i < _Count; i++)
            {
                if (_NeuronTypes.ID == NeuronTypes.NormalNeuron.ID)
                {
                    cNormalNeuron __NormalNeuron = new cNormalNeuron(this);
                    Neurons.Add(__NormalNeuron);
                }
                else if (_NeuronTypes.ID == NeuronTypes.BiasNeuron.ID)
                {
                    cBiasNeuron __NormalNeuron = new cBiasNeuron(this) { Value = m_Random.NextDouble()};
                    Neurons.Add(__NormalNeuron);
                }
            }
        }

        public void FullConnectToLayer(cNeuronLayer _NeuronLayer)
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].ConnectToLayerNeurons(_NeuronLayer);
            }
        }

        public void CalculateNeuronValues()
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].CalculateValue();
            }
            if (NextLayer != null)
            {
                NextLayer.CalculateNeuronValues();
            }
        }


        public double CalculateNeuronError(double[] _Targets)
        {
            double __Result = 0;
            if (NextLayer == null)
            {
                for (int i = 0; i < Neurons.Count; i++)
                {
                    Neurons[i].Error = _Targets[i] - Neurons[i].Value;
                    __Result += Neurons[i].Error;
                }
            }
            else
            {
                for (int i = 0; i < Neurons.Count; i++)
                {
                    Neurons[i].Error = 0;

                    for (int j = 0; j < Neurons[i].Outputs.Count; j++)
                    {
                        Neurons[i].Error += Neurons[i].Outputs[j].EndNeuronErrorWeight;
                        __Result += Neurons[i].Error;
                    }
                }
            }

            if (PreviousLayer != null)
            {
                return __Result + PreviousLayer.CalculateNeuronError(_Targets);
            }
            else
            {
                return __Result;
            }
        }

        public void BackPropagate(double _LearningRate)
        {
            foreach (cBaseNeuron __Neuron in Neurons)
            {
                foreach (cNeuronConnection __InputSynapse in __Neuron.Inputs)
                {
                    var __DeltaWeight = _LearningRate * __Neuron.Error * __Neuron.Value * (1 - __Neuron.Value) * __InputSynapse.StartNeuron.Value;
                    __InputSynapse.Weight += __DeltaWeight;
                }
            }

            if (PreviousLayer != null)
            {
                PreviousLayer.BackPropagate(_LearningRate);
            }
        }
    }
}
