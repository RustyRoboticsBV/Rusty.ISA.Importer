namespace Rusty.CutsceneImporter.InstructionDefinitions
{
    /// <summary>
    /// A list of XML keywords used by instruction definition files.
    /// </summary>
    internal static class Keywords
    {
        /* Public constants. */
        // Definition.
        public const string InstructionDefinition = "definition";

        // Id.
        public const string Opcode = "opcode";
        public const string ID = "id";

        // Parameters.
        public const string BoolParameter = "bool";
        public const string IntParameter = "int";
        public const string IntSliderParameter = "islider";
        public const string FloatParameter = "float";
        public const string FloatSliderParameter = "fslider";
        public const string CharParameter = "char";
        public const string TextParameter = "text";
        public const string MultilineParameter = "multiline";
        public const string ColorParameter = "color";
        public const string OutputParameter = "output";

        public const string DefaultValue = "default";
        public const string MinValue = "min";
        public const string MaxValue = "max";
        public const string RemoveDefaultOutput = "remove_default";
        public const string UseArgumentAsPreview = "use_argument_as_preview";

        // Implementation.
        public const string Implementation = "implementation";

        public const string Members = "members";
        public const string Initialize = "initialize";
        public const string Execute = "execute";

        // Metadata.
        public const string DisplayName = "name";
        public const string Description = "description";
        public const string Icon = "icon";
        public const string Category = "category";

        // Editor node info.
        public const string EditorNodeInfo = "editor_node";

        public const string Priority = "priority";
        public const string MinWidth = "min_width";
        public const string MinHeight = "min_height";
        public const string MainColor = "color_main";
        public const string TextColor = "color_text";

        // Preview.
        public const string PreviewSeparator = "separator";

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
        public const string PreInstructions = "pre";
        public const string PostInstructions = "post";

        public const string InstructionRule = "instruction";
        public const string OptionRule = "option";
        public const string ChoiceRule = "choice";
        public const string TupleRule = "tuple";
        public const string ListRule = "list";

        public const string StartEnabled = "enabled";
        public const string StartSelected = "selected";
        public const string AddButtonText = "button_text";
    }
}