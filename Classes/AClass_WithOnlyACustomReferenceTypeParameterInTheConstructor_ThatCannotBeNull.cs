using System;

namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyACustomReferenceTypeParameterInTheConstructor_ThatCannotBeNull
    {
        public AClass_WithOnlyACustomReferenceTypeParameterInTheConstructor_ThatCannotBeNull(ACustomReferenceType customReferenceType)
        {
            if (customReferenceType == null)
            {
                throw new ArgumentNullException("customReferenceType");
            }
        }

        public AClass_WithOnlyACustomReferenceTypeParameterInTheConstructor_ThatCannotBeNull(ACustomReferenceType_ThatTakesASystemValueTypeInItsConstructor customReferenceType)
        {
            if (customReferenceType == null)
            {
                throw new ArgumentNullException("customReferenceType");
            }
        }
    }
}