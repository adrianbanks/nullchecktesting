using System;

namespace NullCheckTesting
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class CanBeNullAttribute : Attribute
    {}
}
