using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Generator;
using TechTalk.SpecFlow.Generator.UnitTestConverter;

namespace StreamingGeneratorPlugin.SpecFlowPlugin
{
    public class StreamingDecorator : ITestMethodDecorator
    {
        public bool CanDecorateFrom(TestClassGenerationContext generationContext, CodeMemberMethod testMethod)
        {
            return true;
        }

        public void DecorateFrom(TestClassGenerationContext generationContext, CodeMemberMethod testMethod)
        {
            var attribute = new CodeAttributeDeclaration(
                "Xunit.TraitAttribute",
                new CodeAttributeArgument(new CodePrimitiveExpression("Category")),
                new CodeAttributeArgument(new CodePrimitiveExpression("Streaming")));

            testMethod.CustomAttributes.Add(attribute);
        }

        public int Priority { get; }
    }
}
