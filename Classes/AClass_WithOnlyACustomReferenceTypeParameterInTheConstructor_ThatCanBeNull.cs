namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyACustomReferenceTypeParameterInTheConstructor_ThatCanBeNull
    {
        public AClass_WithOnlyACustomReferenceTypeParameterInTheConstructor_ThatCanBeNull([CanBeNull] ACustomReferenceType customReferenceType)
        {
            // no null checks
        }

        public AClass_WithOnlyACustomReferenceTypeParameterInTheConstructor_ThatCanBeNull([CanBeNull] ACustomReferenceType_ThatTakesASystemValueTypeInItsConstructor customReferenceType)
        {
            // no null checks
        }
    }
}