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
            var allTypes = GetType().Assembly.GetTypes().Where(t => !t.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any()).OrderBy(t => t.FullName);

            foreach (var type in allTypes)
            {
                var constructors = type.GetConstructors();

                foreach (var constructor in constructors)
                {
                    var parameters = constructor.GetParameters();

                    if (AllParametersAreValueTypes(parameters))
                    {
                        continue;
                    }

                    // need to run this once for the number of parameters
                    for (int i = 0; i < parameters.Count(); i++)
                    {
                        bool checkingForAnAllowedNull = IsParameterMarkedAsAllowingANull(parameters[i]);
                        object[] parameterValues = CreateParameterValues(parameters);
                        string testDescription = MakeTestDescription(type, parameters, parameterValues);

                        if (checkingForAnAllowedNull)
                        {
                            parameterValues[i] = null;
                            Assert.DoesNotThrow(() => constructor.Invoke(parameterValues), testDescription);
                        }
                        else
                        {
                            try
                            {
                                constructor.Invoke(parameterValues);
                                Assert.Fail("{0}{1}{1}Expected: ArgumentNullException to be thrown{1}But was: No exception was thrown", testDescription, Environment.NewLine);
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

        private bool AllParametersAreValueTypes(IEnumerable<ParameterInfo> parameters)
        {
            return parameters.All(p => p.ParameterType.IsValueType);
        }

        private string MakeTestDescription(Type type, IEnumerable<ParameterInfo> parameters, object[] parameterValues)
        {
            StringBuilder sb = new StringBuilder();
            string parameterTypes = string.Join(", ", parameters.Select(p => p.ParameterType));
            sb.AppendFormat("Checking constructor '{0}' of type '{1}'{2}", parameterTypes, type, Environment.NewLine);
            sb.AppendFormat("    with parameters: {0}{1}", MakeParameterList(parameterValues), Environment.NewLine);
            return sb.ToString();
        }

        private string MakeParameterList(object[] parameterValues)
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

                if (parameter.ParameterType.IsValueType)
                {
                    parameterValues[index++] = Activator.CreateInstance(parameter.ParameterType);
                }
                else if (parameter.ParameterType.IsInterface)
                {
                    parameterValues[index++] = Substitute.For(new[] {parameter.ParameterType}, new object[0]);
                }
            }

            return parameterValues;
        }
    }
}
