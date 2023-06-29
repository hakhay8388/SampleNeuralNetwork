using MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems
{
    public abstract class cBaseNeuron
    {
        ProgressBar ValueProgressBar { get; set; }
        Label ErrorLabel { get; set; }
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

        public void CreateGraphic()
        {
            Point __Point = GetLocation();
            ValueProgressBar = new ProgressBar();
            ValueProgressBar.Size = new Size(150, 15);
            ValueProgressBar.Location = __Point;
            ValueProgressBar.Name = Guid.NewGuid().ToString();
            OwnerNeuronLayer.NeuronLayerManager.AI.OwnerItem.Controls.Add(ValueProgressBar);

            ErrorLabel = new Label();
            ErrorLabel.Size = new Size(150, 20);
            ErrorLabel.Location = new Point(__Point.X, __Point.Y + 20);
            ErrorLabel.Name = Guid.NewGuid().ToString();
            OwnerNeuronLayer.NeuronLayerManager.AI.OwnerItem.Controls.Add(ErrorLabel); 
        }

        public Point GetLocation()
        {
            int __LayerIndex = OwnerNeuronLayer.NeuronLayerManager.NeuronLayers.FindIndex(__Item => __Item == OwnerNeuronLayer);
            int __NeuronIndex = OwnerNeuronLayer.Neurons.FindIndex(__Item => __Item == this);

            int __MaxCount = OwnerNeuronLayer.NeuronLayerManager.NeuronLayers.Max(__Item => __Item.Neurons.Count);
            int __NeuronCount = OwnerNeuronLayer.Neurons.Count;


            //int __TotalHeight = __NeuronCount * 50;

            int __Top = __NeuronIndex * 50;
            int __Left = __LayerIndex * 200;

            return new Point(__Left, __Top);

            /*for (int i = 0; i < OwnerNeuronLayer.NeuronLayerManager.NeuronLayers.Count; i++)
            {
                OwnerNeuronLayer.NeuronLayerManager.NeuronLayers.Max(__Item => __Item.Neurons.Count);
            }*/
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

        public void RefreshValue()
        {
            ValueProgressBar.Value = (int)(Value * 100);
            //ErrorLabel.Text = Error.ToString(); 
            Application.DoEvents();
        }

        public void CalculateValue()
        {
            if (Inputs.Count > 0)
            {
                List<cNeuronConnection> __WeightSynapses = Inputs.Where(__Item => __Item.StartNeuron.NeuronType.ID == NeuronTypes.NormalNeuron.ID && __Item.IsEnabled).ToList();
                List<cNeuronConnection> __BiasSynapseWeight = Inputs.Where(__Item => __Item.StartNeuron.NeuronType.ID == NeuronTypes.BiasNeuron.ID).ToList();

                double __Sum = __WeightSynapses.Sum(__Item => __Item.StartNeuronWeight);
                double __BiasSum = __BiasSynapseWeight.Sum(__Item => __Item.StartNeuronWeight);
                Value = MathHelper.Sigmoid(__Sum + __BiasSum);
            }
            RefreshValue();
        }


    }
}
