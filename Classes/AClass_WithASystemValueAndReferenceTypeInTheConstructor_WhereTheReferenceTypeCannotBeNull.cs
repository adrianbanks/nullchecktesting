using System;

namespace NullCheckTesting.Classes
{
    public class AClass_WithASystemValueAndReferenceTypeInTheConstructor_WhereTheReferenceTypeCannotBeNull
    {
        public AClass_WithASystemValueAndReferenceTypeInTheConstructor_WhereTheReferenceTypeCannotBeNull(int number, string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
        }
    }
}