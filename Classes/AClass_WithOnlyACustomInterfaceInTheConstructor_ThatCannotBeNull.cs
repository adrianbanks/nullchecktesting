using System;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyACustomInterfaceInTheConstructor_ThatCannotBeNull
    {
        public AClass_WithOnlyACustomInterfaceInTheConstructor_ThatCannotBeNull(ACustomInterface comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }
        }
    }
}