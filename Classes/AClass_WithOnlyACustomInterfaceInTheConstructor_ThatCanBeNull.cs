namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyACustomInterfaceInTheConstructor_ThatCanBeNull
    {
        public AClass_WithOnlyACustomInterfaceInTheConstructor_ThatCanBeNull([CanBeNull] ACustomInterface comparer)
        {}
    }
}