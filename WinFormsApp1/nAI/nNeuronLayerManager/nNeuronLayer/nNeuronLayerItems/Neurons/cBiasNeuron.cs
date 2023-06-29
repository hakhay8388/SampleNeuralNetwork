using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons
{
    public class cBiasNeuron : cBaseNeuron
    {
        public cBiasNeuron(cNeuronLayer _OwnerNeuronLayer)
            : base(NeuronTypes.BiasNeuron, _OwnerNeuronLayer)
        {         
        }
    }
}
