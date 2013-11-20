namespace NullCheckTesting.Classes
{
    public class AClass_WithASystemValueAndReferenceTypeInTheConstructor_WhereTheReferenceTypeCanBeNull
    {
        public AClass_WithASystemValueAndReferenceTypeInTheConstructor_WhereTheReferenceTypeCanBeNull(int number, [CanBeNull] string text)
        {
            // no null check
        }
    }
}