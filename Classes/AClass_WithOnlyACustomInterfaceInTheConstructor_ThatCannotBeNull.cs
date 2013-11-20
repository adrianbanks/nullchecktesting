using System;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyACustomInterfaceInTheConstructor_ThatCannotBeNull
    {
        public AClass_WithOnlyACustomInterfaceInTheConstructor_ThatCannotBeNull(ACustomInterface customInterface)
        {
            if (customInterface == null)
            {
                throw new ArgumentNullException("customInterface");
            }
        }
    }
}