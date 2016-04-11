using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic_Network
{
    public class Element:IEnumerable<Int32>
    {
        private List<Int32> Cod_IS_A;   //save cod element, which this element IS_A
        private List<Int32> Cod_HAS_PART;   //save cod element, which this element HAS_PART
        public String Name { get; } //save name of element
        public Int32 Cod { get; }   //save cod of element
        public Element(String name, Int32 cod)
        {
            this.Name = name;
            this.Cod = cod;
            Cod_IS_A = new List<int>();
            Cod_HAS_PART = new List<int>();
        }
        public void AddCod_IS_A(Int32 cod)  //add cod, which this element IS_A
        {
            Cod_IS_A.Add(cod);
        }
        public void AddCod_HAS_PART(Int32 cod)  //add cod, which this element HAS_PART
        {
            Cod_HAS_PART.Add(cod);
        }
        public bool This_IS_A(Int32 cod)    //if this element(without inheritance) is_a cod- return true
        {
            for (int i = 0; i < Cod_IS_A.Count; i++)
                if ((Cod_IS_A[i] == cod)) return true;
            return false;
        }
        public bool This_HAS_PART(Int32 cod)    //if this element(without inheritance) has_part cod- return true
        {
            for (int i = 0; i < Cod_HAS_PART.Count; i++)
                if (Cod_HAS_PART[i] == cod) return true;
            return false;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return Cod_IS_A.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
