using Rusty.Xml;
using Rusty.Cutscenes;

namespace Rusty.CutsceneImporter.InstructionDefinitions
{
    /// <summary>
    /// A class that can combine an instruction set consisting of built-in instruction definitions with a folder containing
    /// user-defined instruction definitions.
    /// </summary>
    public static class InstructionDefinitionCompiler
    {
        /* Public methods. */
        /// <summary>
        /// Convert an instruction definition into an XML document.
        /// </summary>
        public static Document Compile(InstructionDefinition definition)
        {
            // Create root.
            Element root = new Element(Keywords.InstructionDefinition, "");

            // Opcode.
            root.AddChild(new Element(Keywords.Opcode, definition.Opcode));

            // Parameters.
            for (int i = 0; i < definition.Parameters.Length; i++)
            {
                root.AddChild(CompileParameter(definition.Parameters[i]));
            }

            // Implementation.
            root.AddChild(new Element(Keywords.Implementation, definition.Implementation));

            // Meta-data.
            root.AddChild(new Element(Keywords.DisplayName, definition.DisplayName));
            root.AddChild(new Element(Keywords.Description, definition.Description));
            root.AddChild(new Element(Keywords.Category, definition.Category));
            root.AddChild(new Element(Keywords.Icon, definition.Icon.ResourcePath));

            // Editor node.
            if (definition.EditorNode != null)
                root.AddChild(CompileEditorNodeInfo(definition.EditorNode));

            // Default output.
            root.AddChild(new Element(Keywords.HideDefaultOutput, definition.HideDefaultOutput.ToString()));

            // Preview terms.
            for (int i = 0; i < definition.Preview.Length; i++)
            {
                root.AddChild(CompilePreviewTerm(definition.Preview[i]));
            }

            // Compile rules.
            for (int i = 0; i < definition.CompileRules.Length; i++)
            {
                root.AddChild(CompileCompileRule(definition.CompileRules[i]));
            }

            return new Document(definition.ResourceName, root);
        }

        /* Private methods. */
        private static Element CompileParameter(ParameterDefinition parameter)
        {
            Element element = new Element("", "");

            element.Attributes.Add(new Attribute(Keywords.Id, parameter.Id));
            element.AddChild(new Element(Keywords.DisplayName, parameter.DisplayName));
            element.AddChild(new Element(Keywords.Description, parameter.Description));

            switch (parameter)
            {
                case BoolParameter boolParameter:
                    element.Name = Keywords.BoolParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, boolParameter.DefaultValue.ToString()));
                    break;
                case IntParameter intParameter:
                    element.Name = Keywords.IntParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, intParameter.DefaultValue.ToString()));
                    break;
                case FloatParameter floatParameter:
                    element.Name = Keywords.FloatParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, floatParameter.DefaultValue.ToString()));
                    break;
                case IntSliderParameter intSliderParameter:
                    element.Name = Keywords.IntSliderParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, intSliderParameter.DefaultValue.ToString()));
                    element.AddChild(new Element(Keywords.MinValue, intSliderParameter.MinValue.ToString()));
                    element.AddChild(new Element(Keywords.MaxValue, intSliderParameter.MaxValue.ToString()));
                    break;
                case FloatSliderParameter floatSliderParameter:
                    element.Name = Keywords.FloatSliderParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, floatSliderParameter.DefaultValue.ToString()));
                    element.AddChild(new Element(Keywords.MinValue, floatSliderParameter.MinValue.ToString()));
                    element.AddChild(new Element(Keywords.MaxValue, floatSliderParameter.MaxValue.ToString()));
                    break;
                case CharParameter charParameter:
                    element.Name = Keywords.CharParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, charParameter.DefaultValue.ToString()));
                    break;
                case LineParameter lineParameter:
                    element.Name = Keywords.LineParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, lineParameter.DefaultValue.ToString()));
                    break;
                case MultilineParameter multilineParameter:
                    element.Name = Keywords.MultilineParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, multilineParameter.DefaultValue.ToString()));
                    break;
                case ColorParameter colorParameter:
                    element.Name = Keywords.ColorParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, colorParameter.DefaultValue.ToString()));
                    break;
                case OutputParameter outputParameter:
                    element.Name = Keywords.OutputParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, outputParameter.UseParameterAsPreview.ToString()));
                    break;
            }
            return element;
        }

        private static Element CompilePreviewTerm(PreviewTerm term)
        {
            Element element = new Element("", "");

            switch (term.HideIf)
            {
                case HideIf.Never:
                    element.AddChild(new Element(Keywords.HideIf, Keywords.HideIfNever));
                    break;
                case HideIf.PrevIsEmpty:
                    element.AddChild(new Element(Keywords.HideIf, Keywords.HideIfPrevEmpty));
                    break;
                case HideIf.NextIsEmpty:
                    element.AddChild(new Element(Keywords.HideIf, Keywords.HideIfNextEmpty));
                    break;
                case HideIf.EitherIsEmpty:
                    element.AddChild(new Element(Keywords.HideIf, Keywords.HideIfEitherEmpty));
                    break;
                case HideIf.BothAreEmpty:
                    element.AddChild(new Element(Keywords.HideIf, Keywords.HideIfBothEmpty));
                    break;
            }

            switch (term)
            {
                case TextTerm textTerm:
                    element.Name = Keywords.TextTerm;
                    element.AddChild(new Element(Keywords.Text, textTerm.Text));
                    break;
                case ArgumentTerm argTerm:
                    element.Name = Keywords.ArgumentTerm;
                    element.AddChild(new Element(Keywords.ParameterId, argTerm.ParameterId));
                    break;
                case CompileRuleTerm ruleTerm:
                    element.Name = Keywords.CompileRuleTerm;
                    element.AddChild(new Element(Keywords.CompileRuleId, ruleTerm.CompileRuleId));
                    break;
            }
            return element;
        }

        private static Element CompileCompileRule(CompileRule compileRule)
        {
            Element element = new Element("", "");

            element.Attributes.Add(new Attribute(Keywords.Id, compileRule.Id));
            element.AddChild(new Element(Keywords.DisplayName, compileRule.DisplayName));
            element.AddChild(new Element(Keywords.Description, compileRule.Description));

            switch (compileRule)
            {
                case PreInstruction preInstruction:
                    element.Name = Keywords.PreInstruction;
                    element.AddChild(new Element(Keywords.Opcode, preInstruction.Opcode));
                    break;
                case OptionRule optionRule:
                    element.Name = Keywords.OptionRule;
                    element.AddChild(new Element(Keywords.StartEnabled, optionRule.StartEnabled.ToString()));
                    element.AddChild(CompileCompileRule(optionRule.Type));
                    break;
                case ChoiceRule choiceRule:
                    element.Name = Keywords.ChoiceRule;
                    element.AddChild(new Element(Keywords.StartSelected, choiceRule.StartSelected.ToString()));
                    foreach (CompileRule type in choiceRule.Types)
                    {
                        element.AddChild(CompileCompileRule(type));
                    }
                    break;
                case TupleRule tupleRule:
                    element.Name = Keywords.ChoiceRule;
                    foreach (CompileRule type in tupleRule.Types)
                    {
                        element.AddChild(CompileCompileRule(type));
                    }
                    element.AddChild(new Element(Keywords.PreviewSeparator, tupleRule.PreviewSeparator.ToString()));
                    break;
                case ListRule listRule:
                    element.Name = Keywords.ChoiceRule;
                    element.AddChild(CompileCompileRule(listRule.Type));
                    element.AddChild(new Element(Keywords.AddButtonText, listRule.AddButtonText.ToString()));
                    element.AddChild(new Element(Keywords.PreviewSeparator, listRule.PreviewSeparator.ToString()));
                    break;
            }
            return element;
        }

        private static Element CompileEditorNodeInfo(EditorNodeInfo editorNode)
        {
            Element nodeInfo = new Element(Keywords.EditorNodeInfo, "");
            nodeInfo.AddChild(new Element(Keywords.Priority, editorNode.Priority.ToString()));
            nodeInfo.AddChild(new Element(Keywords.MinWidth, editorNode.MinWidth.ToString()));
            nodeInfo.AddChild(new Element(Keywords.MainColor, editorNode.MainColor.ToHtml()));
            nodeInfo.AddChild(new Element(Keywords.TextColor, editorNode.TextColor.ToHtml()));
            return nodeInfo;
        }
    }
}