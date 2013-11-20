namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyASystemReferenceTypeParameterInTheConstructor_ThatCanBeNull
    {
        public AClass_WithOnlyASystemReferenceTypeParameterInTheConstructor_ThatCanBeNull([CanBeNull] string text)
        {
            // no null checks
        }
    }
}