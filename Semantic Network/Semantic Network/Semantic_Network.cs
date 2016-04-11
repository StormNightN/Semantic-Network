using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Semantic_Network
{
    public partial class Semantic_Network
    {
        const int cod_Relation_IS_A = 2;
        const int cod_Relation_HAS_PART = 1;
        private static Dictionary<Int32, Element> elements; //save elements<cod element,element>
        
        public Semantic_Network()
        {
            elements = new Dictionary<int, Element>();
        }    

        public Semantic_Network(StreamReader input)
        {
            elements = new Dictionary<int, Element>();
            input.ReadLine();
            String buf = input.ReadLine();
            while(buf !="#2")   //read elements
            {
                Int32 cod = Convert.ToInt32(buf.Split(':')[0].Trim());
                elements.Add(cod, new Element(buf.Split(':')[1].Trim(), cod));
                buf = input.ReadLine();
            }
            while (buf != "#3") buf = input.ReadLine();
            buf = input.ReadLine();
            while(!input.EndOfStream)
            {
                Int32 element1 = Convert.ToInt32(buf.Split(':')[0].Trim());
                Int32 cod_connect = Convert.ToInt32(buf.Split(':')[1].Trim());
                Int32 element2 = Convert.ToInt32(buf.Split(':')[2].Trim());
                if (cod_connect == cod_Relation_IS_A) elements[element1].AddCod_IS_A(element2);
                else if (cod_connect == cod_Relation_HAS_PART) elements[element1].AddCod_HAS_PART(element2);
                buf = input.ReadLine();
            }
        }

        public void Add_Element(Element el) //add element el to network
        {
            elements.Add(el.Cod, el);
        }

        public void Add_Relation_IS_A(Int32 from, Int32 to)  //add connect from_cod IS_A to_cod
        {
            elements[from].AddCod_IS_A(to);
        }

        public void Add_Relation_HAS_PART(Int32 from, Int32 to)  //add connect from_cod HAS_PART to_cod
        {
            elements[from].AddCod_HAS_PART(to);
        }

        private void PrintRequestResult(Int32 left_cod, Int32 cod_relation, Int32 right_cod, bool result)
        {
            if (cod_relation == cod_Relation_IS_A)
            {
                if (result) Console.WriteLine(elements[Convert.ToInt32(left_cod)].Name + " IS A " +
                                              elements[Convert.ToInt32(right_cod)].Name);
                else Console.WriteLine(elements[Convert.ToInt32(left_cod)].Name + " IS NOT A " +
                                       elements[Convert.ToInt32(right_cod)].Name);
            }
            else if (cod_relation == cod_Relation_HAS_PART)
            {
                if (result) Console.WriteLine(elements[Convert.ToInt32(left_cod)].Name + " HAS PART " +
                                              elements[Convert.ToInt32(right_cod)].Name);
                else Console.WriteLine(elements[Convert.ToInt32(left_cod)].Name + " HAS NOT PART " +
                                       elements[Convert.ToInt32(right_cod)].Name);
            }
        }

        public void PrintRequest(String request)
        {
            String S_left_cod = request.Split(':')[0];
            String S_cod_relation = request.Split(':')[1];
            String S_right_cod = request.Split(':')[2];
            if ((S_left_cod != "?") && (S_cod_relation != "?") && (S_right_cod != "?"))   //x:x:x
            {
                Int32 cod_relation = Convert.ToInt32(S_cod_relation);
                Int32 left_cod = Convert.ToInt32(S_left_cod);
                Int32 right_cod = Convert.ToInt32(S_right_cod);
                bool result = Request(left_cod, cod_relation, right_cod);
                PrintRequestResult(left_cod, cod_relation, right_cod, result);
            }
            else
            {
                IEnumerable<int> leftElement, relation, rightElement;
                if (S_left_cod == "?") leftElement = new UdefineIterElements();
                else leftElement = new defineIter(Convert.ToInt32(S_left_cod));
                if (S_cod_relation == "?") relation = new UdefineIterRelation();
                else relation = new defineIter(Convert.ToInt32(S_cod_relation));
                if (S_right_cod == "?") rightElement = new UdefineIterElements();
                else rightElement = new defineIter(Convert.ToInt32(S_right_cod));
                foreach(int left_cod in leftElement)
                    foreach(int cod_relation in relation)
                        foreach(int right_cod in rightElement)
                        {
                            bool result = Request(left_cod, cod_relation, right_cod);
                            if(result) PrintRequestResult(left_cod, cod_relation, right_cod, result);
                        }
            }
        }

        private bool Request(Int32 left_cod, Int32 cod_relation, Int32 right_cod)
        {
            if(cod_relation==cod_Relation_IS_A) return Request_IS_A(left_cod, right_cod);
            else if(cod_relation==cod_Relation_HAS_PART) return Request_HAS_PART(left_cod, right_cod);
            return false;
        }

        private bool Request_IS_A(Int32 cod1, Int32 cod2, int step=0) //cod1 IS_A cod2
        {
            if (step == elements.Count) return false;
            if (elements[cod1].This_IS_A(cod2)) return true;
            foreach(int cod_IS_A in elements[cod1])
                if (Request_IS_A(cod_IS_A, cod2, step + 1)) return true;
            return false;
        }

        private bool Request_HAS_PART(Int32 cod1, Int32 cod2, int step=0) //cod1 HAS_PART cod2
        {
            if (step == elements.Count) return false;
            if (elements[cod1].This_HAS_PART(cod2)) return true;
            foreach (int cod_IS_A in elements[cod1])
                if (Request_HAS_PART(cod_IS_A, cod2, step + 1)) return true;
            return false;
        }
    }
}
