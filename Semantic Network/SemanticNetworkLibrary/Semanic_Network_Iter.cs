using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semantic_Network
{
    public partial class Semantic_Network
    {
        private class defineIter : IEnumerable<int> //for x
        {
            int id;
            public defineIter(int i) { id = i; }
            public IEnumerator<int> GetEnumerator() { return GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { yield return id; }
        }

        private class UdefineIterRelation:IEnumerable<int>  //for Undefain relation in request
        {
            public UdefineIterRelation() { }
            public IEnumerator<int> GetEnumerator()
            {
                yield return cod_Relation_IS_A;
                yield return cod_Relation_HAS_PART;
            }
            IEnumerator IEnumerable.GetEnumerator()
            {
                yield return cod_Relation_IS_A;
                yield return cod_Relation_HAS_PART;
            }
        }

        private class UdefineIterElements : IEnumerable<int>    //for Undefain element in request
        {
            public UdefineIterElements() { }
            public IEnumerator<int> GetEnumerator() { return elements.Keys.GetEnumerator(); }
            IEnumerator IEnumerable.GetEnumerator() { return elements.Keys.GetEnumerator(); }
        }
    }
}
