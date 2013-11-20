using System.Collections.Generic;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyAGenericSystemInterfaceInTheConstructor_ThatCanBeNull
    {
        public AClass_WithOnlyAGenericSystemInterfaceInTheConstructor_ThatCanBeNull([CanBeNull] IList<int> list)
        {}
    }
}