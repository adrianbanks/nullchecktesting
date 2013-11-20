using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace NullCheckTesting
{
    [TestFixture]
    public sealed class ArgNullCheckTest
    {
        [Test]
        public void TestThat_AllConstructors_CheckForNullArguments_WhenNotExplicitlyMarkedAsAllowingANull()
        {
            var allTypes = GetAllTypesToCheck();

            foreach (var type in allTypes)
            {
                var constructors = type.GetConstructors();

                foreach (var constructor in constructors)
                {
                    var parameters = constructor.GetParameters();

                    if (!parameters.Any())
                    {
                        Trace("Skipping checking constructor: .ctor()");
                        Trace("  of type: {0}", type);
                        Trace("  because it has no parameters");
                        Trace();
                        continue;
                    }

                    if (AllParametersAreValueTypes(parameters))
                    {
                        string parameterTypes = string.Join(", ", parameters.Select(p => p.ParameterType.Name));
                        Trace("Skipping checking constructor: .ctor({0})", parameterTypes);
                        Trace("  of type: {0}", type.Name);
                        Trace("  because it uses only value types");
                        Trace();
                        continue;
                    }

                    // need to run this once for the number of parameters
                    for (int i = 0; i < parameters.Count(); i++)
                    {
                        var parameter = parameters[i];

                        if (parameter.ParameterType.IsValueType)
                        {
                            string parameterTypes = string.Join(", ", parameters.Select(p => p.ParameterType.Name));
                            Trace("Skipping checking constructor: .ctor({0})", parameterTypes);
                            Trace("  of type: {0}", type.Name);
                            Trace("  for parameter: {0}, {1}", i, parameter.ParameterType);
                            Trace("  because the parameter being tested is a value type");
                            Trace();
                            continue;
                        }

                        bool checkingForAnAllowedNull = IsParameterMarkedAsAllowingANull(parameter);
                        object[] parameterValues = CreateParameterValues(parameters);
                        
                        // set the current parameter value being tested to be null
                        parameterValues[i] = null;

                        string testDescription = MakeTestDescription(type, parameters, parameterValues);
                        Trace(testDescription);

                        if (checkingForAnAllowedNull)
                        {
                            Assert.DoesNotThrow(() => constructor.Invoke(parameterValues), testDescription);
                        }
                        else
                        {
                            try
                            {
                                constructor.Invoke(parameterValues);
                                Assert.Fail("{0}{1}Expected: ArgumentNullException to be thrown{1}But was: No exception was thrown", testDescription, Environment.NewLine);
                            }
                            catch (TargetInvocationException exception)
                            {
                                Exception innerException = exception.InnerException;
                                Assert.That(innerException, Is.InstanceOf<ArgumentNullException>(), testDescription);
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<Type> GetAllTypesToCheck()
        {
            var allTypes = GetType().Assembly.GetTypes();

            // remove any anonymous types
            var typesToCheck = allTypes.Where(t => !t.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any());

            // remove any abstract classes
            typesToCheck = typesToCheck.Where(t => !t.IsAbstract);

            // remove any interfaces
            typesToCheck = typesToCheck.Where(t => !t.IsInterface);
                
            return typesToCheck.OrderBy(t => t.FullName);
        }

        private bool AllParametersAreValueTypes(IEnumerable<ParameterInfo> parameters)
        {
            return parameters.All(p => p.ParameterType.IsValueType);
        }

        private string MakeTestDescription(Type type, IEnumerable<ParameterInfo> parameters, object[] parameterValues)
        {
            StringBuilder sb = new StringBuilder();
            string parameterTypes = string.Join(", ", parameters.Select(p => p.ParameterType.Name));
            sb.AppendFormat("Checking constructor: .ctor({0}){1}", parameterTypes, Environment.NewLine);
            sb.AppendFormat("  of type: {0}{1}", type.Name, Environment.NewLine);
            sb.AppendFormat("  with parameters: {0}{1}", MakeParameterValueList(parameterValues), Environment.NewLine);
            return sb.ToString();
        }

        private string MakeParameterValueList(object[] parameterValues)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < parameterValues.Length; i++)
            {
                var parameterValue = parameterValues[i];
                sb.Append(parameterValue ?? "<null>");

                if (i != parameterValues.Length - 1)
                {
                    sb.Append(", ");
                }
            }

            return sb.ToString();
        }

        private bool IsParameterMarkedAsAllowingANull(ParameterInfo parameter)
        {
            return parameter.GetCustomAttributes(typeof(CanBeNullAttribute), false).Any();
        }

        private object[] CreateParameterValues(ParameterInfo[] parameters)
        {
            object[] parameterValues = new object[parameters.Length];

            foreach (var parameterInfo in parameters.Select((p, i) => new {Parameter = p, Index = i}))
            {
                var parameter = parameterInfo.Parameter;
                int index = parameterInfo.Index;
                object parameterValue = null;

                if (parameter.ParameterType == typeof(string))
                {
                    // string is a value type, but can still be null so we have to make a string
                    parameterValue = "string";
                }
                else if (parameter.ParameterType.IsValueType)
                {
                    // value types can just take their default values
                    parameterValue = Activator.CreateInstance(parameter.ParameterType);
                }
                else if (parameter.ParameterType.IsInterface)
                {
                    // we have to make a proper instance of an interface
                    parameterValue = Substitute.For(new[] {parameter.ParameterType}, new object[0]);
                }
                else
                {
                    // reference types need to be constructed
                }

                parameterValues[index++] = parameterValue;
            }

            return parameterValues;
        }

        private const bool traceEnabled = true;

        private void Trace()
        {
            if (traceEnabled)
            {
                Console.WriteLine();
            }
        }

        private void Trace(string format, params object[] args)
        {
            if (traceEnabled)
            {
                Console.WriteLine(format, args);
            }
        }
    }
}
