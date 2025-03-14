using Rusty.Xml;
using Rusty.Cutscenes;

namespace Rusty.CutsceneImporter.InstructionDefinitions
{
    /// <summary>
    /// A class that can serialize an instruction definition into an XML document.
    /// </summary>
    public static class Serializer
    {
        /* Public methods. */
        /// <summary>
        /// Convert an instruction definition into an XML document.
        /// </summary>
        public static Document Serialize(InstructionDefinition definition)
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
            if (definition.Implementation != null)
                root.AddChild(CompileImplementation(definition.Implementation));

            // Meta-data.
            if (definition.Icon != null)
                root.AddChild(new Element(Keywords.Icon, definition.Icon.ResourcePath));
            root.AddChild(new Element(Keywords.DisplayName, definition.DisplayName));
            root.AddChild(new Element(Keywords.Description, definition.Description));
            root.AddChild(new Element(Keywords.Category, definition.Category));

            // Editor node.
            if (definition.EditorNode != null)
                root.AddChild(CompileEditorNodeInfo(definition.EditorNode));

            // Preview.
            for (int i = 0; i < definition.PreviewTerms.Length; i++)
            {
                root.AddChild(CompilePreviewTerm(definition.PreviewTerms[i]));
            }

            // Pre-instructions.
            if (definition.PreInstructions.Length > 0)
                root.AddChild(CompileRules(Keywords.PreInstructions, definition.PreInstructions));

            // Post-instructions.
            if (definition.PostInstructions.Length > 0)
                root.AddChild(CompileRules(Keywords.PostInstructions, definition.PostInstructions));

            // Return finished document.
            return new Document(definition.ResourceName, root);
        }

        /* Private methods. */
        private static Element CompileParameter(Parameter parameter)
        {
            Element element = new Element("", "");

            element.Attributes.Add(new Attribute(Keywords.ID, parameter.ID));
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
                case TextParameter TextParameter:
                    element.Name = Keywords.TextParameter;
                    element.AddChild(new Element(Keywords.DefaultValue, TextParameter.DefaultValue.ToString()));
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
                    element.AddChild(new Element(Keywords.DefaultValue, outputParameter.UseArgumentAsPreview.ToString()));
                    break;
            }
            return element;
        }

        private static Element CompileImplementation(Implementation implementation)
        {
            Element element = new Element("implementation", "");

            if (implementation.Members != "")
                element.AddChild(new Element(Keywords.Members, implementation.Members));
            if (implementation.Initialize != "")
                element.AddChild(new Element(Keywords.Initialize, implementation.Initialize));
            if (implementation.Execute != "")
                element.AddChild(new Element(Keywords.Execute, implementation.Execute));

            return element;
        }

        private static Element CompileEditorNodeInfo(EditorNodeInfo editorNode)
        {
            Element nodeInfo = new Element(Keywords.EditorNodeInfo, "");
            nodeInfo.AddChild(new Element(Keywords.Priority, editorNode.Priority.ToString()));
            nodeInfo.AddChild(new Element(Keywords.MinWidth, editorNode.MinWidth.ToString()));
            nodeInfo.AddChild(new Element(Keywords.MainColor, '#' + editorNode.MainColor.ToHtml()));
            nodeInfo.AddChild(new Element(Keywords.TextColor, '#' + editorNode.TextColor.ToHtml()));
            return nodeInfo;
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
                    element.AddChild(new Element(Keywords.ParameterId, argTerm.ParameterID));
                    break;
                case CompileRuleTerm ruleTerm:
                    element.Name = Keywords.CompileRuleTerm;
                    element.AddChild(new Element(Keywords.CompileRuleId, ruleTerm.RuleID));
                    break;
            }
            return element;
        }

        private static Element CompileRules(string elementName, CompileRule[] rules)
        {
            Element element = new Element(elementName, "");
            for (int i = 0; i < rules.Length; i++)
            {
                element.AddChild(CompileRule(rules[i]));
            }
            return element;
        }

        private static Element CompileRule(CompileRule compileRule)
        {
            Element element = new Element("", "");

            element.Attributes.Add(new Attribute(Keywords.ID, compileRule.ID));
            element.AddChild(new Element(Keywords.DisplayName, compileRule.DisplayName));
            element.AddChild(new Element(Keywords.Description, compileRule.Description));

            switch (compileRule)
            {
                case InstructionRule instruction:
                    element.Name = Keywords.InstructionRule;
                    element.AddChild(new Element(Keywords.Opcode, instruction.Opcode));
                    break;
                case OptionRule optionRule:
                    element.Name = Keywords.OptionRule;
                    element.AddChild(new Element(Keywords.StartEnabled, optionRule.StartEnabled.ToString()));
                    element.AddChild(CompileRule(optionRule.Type));
                    break;
                case ChoiceRule choiceRule:
                    element.Name = Keywords.ChoiceRule;
                    element.AddChild(new Element(Keywords.StartSelected, choiceRule.StartSelected.ToString()));
                    foreach (CompileRule type in choiceRule.Types)
                    {
                        element.AddChild(CompileRule(type));
                    }
                    break;
                case TupleRule tupleRule:
                    element.Name = Keywords.ChoiceRule;
                    foreach (CompileRule type in tupleRule.Types)
                    {
                        element.AddChild(CompileRule(type));
                    }
                    element.AddChild(new Element(Keywords.PreviewSeparator, tupleRule.PreviewSeparator.ToString()));
                    break;
                case ListRule listRule:
                    element.Name = Keywords.ChoiceRule;
                    element.AddChild(CompileRule(listRule.Type));
                    element.AddChild(new Element(Keywords.AddButtonText, listRule.AddButtonText.ToString()));
                    element.AddChild(new Element(Keywords.PreviewSeparator, listRule.PreviewSeparator.ToString()));
                    break;
            }
            return element;
        }
    }
}