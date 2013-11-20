namespace NullCheckTesting.Classes
{
    public class AClass_WithOnlyAGenericCustomInterfaceInTheConstructor_ThatCanBeNull
    {
        public AClass_WithOnlyAGenericCustomInterfaceInTheConstructor_ThatCanBeNull([CanBeNull] ACustomGenericInterface<int> customGenericInterface)
        {}
    }
}