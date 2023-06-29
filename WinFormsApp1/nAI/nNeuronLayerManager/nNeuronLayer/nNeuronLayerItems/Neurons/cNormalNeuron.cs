using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons
{
    public class cNormalNeuron : cBaseNeuron
    {
        public cNormalNeuron(cNeuronLayer _OwnerNeuronLayer)
            : base(NeuronTypes.NormalNeuron, _OwnerNeuronLayer)
        {         
        }
    }
}
