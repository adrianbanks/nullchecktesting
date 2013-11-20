using System;
using System.Collections;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyASystemInterfaceInTheConstructor_ThatCannotBeNull
    {
        public AClass_WithOnlyASystemInterfaceInTheConstructor_ThatCannotBeNull(IComparer comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
        }
    }
}