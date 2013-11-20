using System.Collections;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyASystemInterfaceInTheConstructor_ThatCanBeNull
    {
        public AClass_WithOnlyASystemInterfaceInTheConstructor_ThatCanBeNull([CanBeNull] IComparer comparer)
        {}
    }
}