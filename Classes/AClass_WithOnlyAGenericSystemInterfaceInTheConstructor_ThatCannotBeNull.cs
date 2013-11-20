using System;
using System.Collections.Generic;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyAGenericSystemInterfaceInTheConstructor_ThatCannotBeNull
    {
        public AClass_WithOnlyAGenericSystemInterfaceInTheConstructor_ThatCannotBeNull(IList<int> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }
        }
    }
}