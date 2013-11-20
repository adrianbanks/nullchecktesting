using System;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyASystemReferenceTypeParameterInTheConstructor_ThatCannotBeNull
    {
        public AClass_WithOnlyASystemReferenceTypeParameterInTheConstructor_ThatCannotBeNull(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
        }
    }
}