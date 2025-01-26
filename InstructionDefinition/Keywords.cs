namespace Rusty.CutsceneImporter.InstructionDefinitions
{
    /// <summary>
    /// A list of XML keywords used by instruction definition files.
    /// </summary>
    internal static class Keywords
    {
        /* Public constants. */
        // Definition.
        public const string InstructionDefinition = "instruction_definition";

        // Id.
        public const string Opcode = "opcode";
        public const string Id = "id";

        // Parameters.
        public const string BoolParameter = "bool";
        public const string IntParameter = "int";
        public const string IntSliderParameter = "islider";
        public const string FloatParameter = "float";
        public const string FloatSliderParameter = "fslider";
        public const string CharParameter = "char";
        public const string LineParameter = "line";
        public const string MultilineParameter = "multiline";
        public const string ColorParameter = "color";
        public const string OutputParameter = "output";

        public const string DefaultValue = "default";
        public const string MinValue = "min";
        public const string MaxValue = "max";
        public const string UseArgumentAsLabel = "use_argument_as_label";

        // Implementation.
        public const string Implementation = "implementation";

        // Metadata.
        public const string DisplayName = "name";
        public const string Description = "description";
        public const string Icon = "icon";
        public const string Category = "category";

        // Editor node info.
        public const string EditorNodeInfo = "editor_node";

        public const string Priority = "priority";
        public const string MinWidth = "min_width";
        public const string MainColor = "color_main";
        public const string TextColor = "color_text";

        // Default output.
        public const string HideDefaultOutput = "hide_default_output";

        // Preview terms.
        public const string TextTerm = "text_term";
        public const string ArgumentTerm = "argument_term";
        public const string CompileRuleTerm = "rule_term";

        public const string Text = "text";
        public const string ParameterId = "parameter";
        public const string CompileRuleId = "rule";
        public const string HideIf = "hide_if";

        public const string HideIfNever = "never";
        public const string HideIfPrevEmpty = "prev_is_empty";
        public const string HideIfNextEmpty = "next_is_empty";
        public const string HideIfEitherEmpty = "either_is_empty";
        public const string HideIfBothEmpty = "both_are_empty";

        // Compile rules.
        public const string PreInstruction = "pre_instruction";
        public const string OptionRule = "option";
        public const string ChoiceRule = "choice";
        public const string TupleRule = "tuple";
        public const string ListRule = "list";

        public const string StartEnabled = "enabled";
        public const string StartSelected = "selected";
        public const string AddButtonText = "button_text";
        public const string PreviewSeparator = "preview_separator";
    }
}
