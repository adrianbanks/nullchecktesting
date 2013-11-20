using System;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyAGenericCustomInterfaceInTheConstructor_ThatCannotBeNull
    {
        public AClass_WithOnlyAGenericCustomInterfaceInTheConstructor_ThatCannotBeNull(ACustomGenericInterface<int> customGenericInterface)
        {
            if (customGenericInterface == null)
            {
                throw new ArgumentNullException("customGenericInterface");
            }
        }
    }
}