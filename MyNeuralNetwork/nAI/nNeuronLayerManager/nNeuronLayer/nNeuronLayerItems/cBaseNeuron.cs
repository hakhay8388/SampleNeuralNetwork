using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems
{
    public abstract class cBaseNeuron
    {
        public NeuronTypes NeuronType { get; set; }
        public cNeuronLayer OwnerNeuronLayer { get; set; }
        public double Value { get; set; }
        public double Error { get; set; }

        public List<cNeuronConnection> Inputs { get; set; }

        public List<cNeuronConnection> Outputs { get; set; }

        public cBaseNeuron(NeuronTypes _NeuronType, cNeuronLayer _OwnerNeuronLayer)
        {
            Inputs = new List<cNeuronConnection>();
            Outputs = new List<cNeuronConnection>();
            OwnerNeuronLayer = _OwnerNeuronLayer;
            NeuronType = _NeuronType;
        }

        public void ConnectToLayerNeurons(cNeuronLayer _NeuronLayer)
        {
            Random m_Random = new Random();
            for (int i = 0; i < _NeuronLayer.Neurons.Count; i++)
            {
                if (_NeuronLayer.Neurons[i].NeuronType.ID != NeuronTypes.BiasNeuron.ID)
                {
                    cNeuronConnection __NeuronConnection = new cNeuronConnection(this, _NeuronLayer.Neurons[i], m_Random.NextDouble());
                }
            }
        }

        public void CalculateValue()
        {
            if (Inputs.Count > 0)
            {
                List<cNeuronConnection> __WeightSynapses = Inputs.Where(__Item => __Item.StartNeuron.NeuronType.ID == NeuronTypes.NormalNeuron.ID).ToList();
                List<cNeuronConnection> __BiasSynapseWeight = Inputs.Where(__Item => __Item.StartNeuron.NeuronType.ID == NeuronTypes.BiasNeuron.ID).ToList();

                double __Sum = __WeightSynapses.Sum(__Item => __Item.StartNeuronWeight);
                double __BiasSum = __BiasSynapseWeight.Sum(__Item => __Item.StartNeuronWeight);
                Value = MathHelper.Sigmoid(__Sum + __BiasSum);
            }
        }
    }
}
