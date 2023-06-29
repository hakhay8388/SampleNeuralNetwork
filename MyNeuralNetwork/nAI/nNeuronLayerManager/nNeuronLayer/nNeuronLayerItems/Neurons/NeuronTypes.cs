using MyNeuralNetwork.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeuralNetwork.nAI.nNeuronLayerManager.nNeuronLayer.nNeuronLayerItems.Neurons
{
    public class NeuronTypes : cBaseConstType<NeuronTypes>
    {
        public static NeuronTypes NormalNeuron = new NeuronTypes(GetVariableName(() => NormalNeuron), 1);
        public static NeuronTypes BiasNeuron = new NeuronTypes(GetVariableName(() => BiasNeuron),2);

        static List<NeuronTypes> TypeList { get; set; }
        public NeuronTypes(string _Name, int _Value)
            : base(_Name, _Name, _Value)
        {
            TypeList = TypeList ?? new List<NeuronTypes>();
            TypeList.Add(this);
        }
       
        public static NeuronTypes GetByID(int _ID, NeuronTypes _DefaultBootType)
        {
            return GetByID(TypeList, _ID, _DefaultBootType);
        }
        public static NeuronTypes GetByName(string _Name, NeuronTypes _DefaultBootType)
        {
            return GetByName(TypeList, _Name, _DefaultBootType);
        }
    }
}
