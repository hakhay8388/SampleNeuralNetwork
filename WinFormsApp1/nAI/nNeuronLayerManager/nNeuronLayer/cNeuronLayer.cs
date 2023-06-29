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
        public bool IsInputLayer { get { return PreviousLayer == null; } }
        public bool IsOutLayer { get { return NextLayer == null; } }

        public bool IsHidden { get { return !IsInputLayer && !IsOutLayer; } }
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
                    double __BiasValue = (m_Random.NextDouble() * ((1.0f / Neurons.Count) - 0.0001)) + 0.0001;
                    //double __BiasValue = (1.0d / Neurons.Count) * 0.85;
                    cBiasNeuron __NormalNeuron = new cBiasNeuron(this) { Value = __BiasValue };
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
            if (IsOutLayer)
            {
                for (int i = 0; i < Neurons.Count; i++)
                {
                    Neurons[i].Error = _Targets[i] - Neurons[i].Value;
                    __Result += Math.Abs(Neurons[i].Error);
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

                        //__Result += Math.Abs(Neurons[i].Error);
                    }
                }
            }

            if (IsInputLayer)
            {
                return __Result;
            }
            else
            {
                return __Result + PreviousLayer.CalculateNeuronError(_Targets); 
            }
        }

       /* public void ReduceBias(double _Error)
        {
            List<cBaseNeuron> __BiasNeuros = Neurons.Where(__Item => __Item.NeuronType.ID == NeuronTypes.BiasNeuron.ID).ToList();
            for (int i = 0; i < __BiasNeuros.Count; i++)
            {
                double __Result = __BiasNeuros[i].Outputs.Sum(__Item => __Item.EndNeuron.Error) / 2;
                if (__Result < 0)
                {
                    __Result = 0;
                }
                else if (__Result > 1)
                {
                    __Result = 1;
                }

                __BiasNeuros[i].Value = __Result;
            }

            if (!IsOutLayer)
            {
                NextLayer.ReduceBias(_Error);
            }
        }*/

        public void BackPropagate(double _LearningRate)
        {
            foreach (cBaseNeuron __Neuron in Neurons)
            {
                foreach (cNeuronConnection __InputSynapse in __Neuron.Inputs)
                {
                    var __DeltaWeight = _LearningRate * __Neuron.Error * __Neuron.Value * (1 - __Neuron.Value) * __InputSynapse.StartNeuron.Value;
                    __InputSynapse.Weight += __DeltaWeight;
                }

                //__Neuron.Inputs.RemoveAll(__Item => __Item.IsDeletable);
            }

            if (PreviousLayer != null)
            {
                PreviousLayer.BackPropagate(_LearningRate);
            }
        }

        public void DrawLayer()
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].CreateGraphic();
            }
        }

    }
}
