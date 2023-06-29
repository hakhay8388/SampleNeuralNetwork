using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems
{
    public class cNeuronConnection
    {
        public double Weight { get; set; }
        public cBaseNeuron StartNeuron { get; set; }
        public cBaseNeuron EndNeuron { get; set; }

        public bool IsEnabled { get; set; }

        public cNeuronConnection(cBaseNeuron _StartNeuron, cBaseNeuron _EndNeuron, double _Weight)
        {
            StartNeuron = _StartNeuron;
            EndNeuron = _EndNeuron;
            Weight = _Weight;
            IsEnabled = true;
            Init();
        }

        private void Init()
        {
            StartNeuron.Outputs.Add(this);
            EndNeuron.Inputs.Add(this);
        }

        public double StartNeuronWeight
        {
            get
            {
                return Weight * StartNeuron.Value;
            }
        }

        public double EndNeuronErrorWeight
        {
            get
            {
                return Weight * EndNeuron.Error;
            }
        }
    }
}
