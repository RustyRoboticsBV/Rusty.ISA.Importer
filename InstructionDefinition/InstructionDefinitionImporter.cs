using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using Rusty.Xml;
using Rusty.Cutscenes;

namespace Rusty.CutsceneImporter.InstructionDefinitions
{
    /// <summary>
    /// An importer for XML-based cutscene instruction definitions.
    /// </summary>
    [Tool]
    [GlobalClass]
    public partial class InstructionDefinitionImporter : Node
    {

        /* Public constants. */
        #region XML_CONSTANTS
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
        public const string LineParameter = "line";
        public const string MultilineParameter = "multiline";
        public const string ColorParameter = "color";
        public const string OutputParameter = "output";

        public const string DefaultValue = "default";
        public const string MinValue = "min";
        public const string MaxValue = "max";
        public const string OverrideDefaultOutput = "override_default_output";
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
        public const string OptionRule = "option";
        public const string ChoiceRule = "choice";
        public const string TupleRule = "tuple";
        public const string ListRule = "list";
        public const string PreInstruction = "pre_instruction";

        public const string StartEnabled = "enabled";
        public const string StartSelected = "selected";
        public const string AddButtonText = "button_text";
        public const string PreviewSeparator = "preview_separator";
        #endregion

        /* Public methods. */
        /// <summary>
        /// Load an instruction definition from some file path.
        /// </summary>
        public static InstructionDefinition Import(string xmlFilePath, Dictionary importOptions)
        {
            return Import(xmlFilePath);
        }

        /* Private methods. */
        /// <summary>
        /// Load an instruction definition from some file path.
        /// </summary>
        private static InstructionDefinition Import(string filePath)
        {
            // Get global file & folder paths.
            filePath = ProjectSettings.GlobalizePath(filePath);
            string folderPath = Path.GetDirectoryName(filePath);

            // Load XML document.
            Document document = new Document(filePath);

            // Init constructor arguments.
            ConstructorArgs args = new ConstructorArgs();

            // Find parameters, preview terms & compile rules.
            for (int i = 0; i < document.Root.Children.Count; i++)
            {
                Element element = document.Root.Children[i];

                switch (element.Name)
                {
                    case Opcode:
                        args.opcode = element.InnerText;
                        break;

                    case BoolParameter:
                        args.parameters.Add(new BoolParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetBoolChild(element, DefaultValue)
                        ));
                        break;
                    case IntParameter:
                        args.parameters.Add(new IntParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetIntChild(element, DefaultValue)
                        ));
                        break;
                    case IntSliderParameter:
                        args.parameters.Add(new IntSliderParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetIntChild(element, DefaultValue),
                            GetIntChild(element, MinValue),
                            GetIntChild(element, MaxValue)
                        ));
                        break;
                    case FloatParameter:
                        args.parameters.Add(new FloatParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetFloatChild(element, DefaultValue)
                        ));
                        break;
                    case FloatSliderParameter:
                        args.parameters.Add(new FloatSliderParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetFloatChild(element, DefaultValue),
                            GetFloatChild(element, MinValue),
                            GetFloatChild(element, MaxValue)
                        ));
                        break;
                    case LineParameter:
                        args.parameters.Add(new LineParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetStringChild(element, DefaultValue)
                        ));
                        break;
                    case MultilineParameter:
                        args.parameters.Add(new MultilineParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetStringChild(element, DefaultValue)
                        ));
                        break;
                    case ColorParameter:
                        args.parameters.Add(new ColorParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetColorChild(element, DefaultValue)
                        ));
                        break;
                    case OutputParameter:
                        args.parameters.Add(new OutputParameter(GetId(element),
                            GetStringChild(element, DisplayName),
                            GetStringChild(element, Description),
                            GetStringChild(element, UseArgumentAsLabel)
                        ));
                        break;

                    case Implementation:
                        args.implementation = ProcessImplementation(element.InnerText);
                        break;

                    case Icon:
                        args.icon = GetTexture(folderPath, element.InnerText);
                        break;
                    case DisplayName:
                        args.displayName = element.InnerText;
                        break;
                    case Description:
                        args.description = element.InnerText;
                        break;
                    case Category:
                        args.category = element.InnerText;
                        break;

                    case EditorNodeInfo:
                        EditorNodeInfo defaults = new EditorNodeInfo();
                        int priority = GetIntChild(element, Priority, defaults.Priority);
                        int minWidth = GetIntChild(element, MinWidth, defaults.MinWidth);
                        Color mainColor = GetColorChild(element, MainColor, defaults.MainColor);
                        Color textColor = GetColorChild(element, TextColor, defaults.TextColor);
                        args.editorNodeInfo = new EditorNodeInfo(priority, minWidth, mainColor, textColor);
                        break;

                    case HideDefaultOutput:
                        args.hideDefaultOutput = true;
                        break;

                    case TextTerm:
                    case ArgumentTerm:
                    case CompileRuleTerm:
                        break;

                    case OptionRule:
                        args.compileRules.Add(ParseOption(element));
                        break;
                    case ChoiceRule:
                        args.compileRules.Add(ParseChoice(element));
                        break;
                    case TupleRule:
                        args.compileRules.Add(ParseTuple(element));
                        break;
                    case ListRule:
                        args.compileRules.Add(ParseList(element));
                        break;

                    default:
                        throw new Exception($"Encountered XML element with name '{element.Name}' in instruction definition file " +
                            $"'{filePath}'. This name is not allowed.");
                }
            }

            // Create instruction definition.
            return new InstructionDefinition(
                args.opcode, args.parameters.ToArray(),
                args.implementation,
                args.icon, args.displayName, args.description, args.category,
                args.editorNodeInfo, args.hideDefaultOutput, args.previewTerms.ToArray(), args.compileRules.ToArray()
            );
        }

        /// <summary>
        /// Process a string of implementation code such that it is ready to be read.
        /// </summary>
        private static string ProcessImplementation(string code)
        {
            // Replace line-breaks with UNIX-style.
            code = code.Replace("\r\n", "\n");
            code = code.Replace("\r", "\n");

            while (code.StartsWith("\n"))
            {
                code = code.Substring(1);
            }

            // Remove indentation.
            int indent = 0;
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == ' ')
                    indent++;
                else
                    break;
            }
            code = code.Substring(indent).Replace("\n" + new string(' ', indent), "\n");

            return code;
        }


        private static string GetId(Element element, string defaultValue = "")
        {
            try
            {
                return element.GetAttribute(Id);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static bool GetBoolChild(Element element, string name, bool defaultValue = default)
        {
            try
            {
                return bool.Parse(element.GetChild(name).InnerText);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static int GetIntChild(Element element, string name, int defaultValue = default)
        {
            try
            {
                return int.Parse(element.GetChild(name).InnerText);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static float GetFloatChild(Element element, string name, float defaultValue = default)
        {
            try
            {
                return float.Parse(element.GetChild(name).InnerText);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static string GetStringChild(Element element, string name, string defaultValue = "")
        {
            try
            {
                return element.GetChild(name).InnerText;
            }
            catch
            {
                return defaultValue;
            }
        }

        private static Color GetColorChild(Element element, string name, Color defaultValue = default)
        {
            try
            {
                return Color.FromHtml(element.GetChild(name).InnerText);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static Texture2D GetTexture(string folderPath, string localFilePath)
        {
            try
            {
                string localPath = localFilePath;
                string globalPath = folderPath + "\\" + localPath;

                Image image = new();
                image.Load(globalPath);

                return ImageTexture.CreateFromImage(image);
            }
            catch
            {
                return null;
            }
        }


        private static CompileRule ParseCompileRule(Element element)
        {
            switch (element.Name)
            {
                case DisplayName:
                case Description:
                case PreviewSeparator:
                case AddButtonText:
                    return null;
                case OptionRule:
                    return ParseOption(element);
                case ChoiceRule:
                    return ParseChoice(element);
                case TupleRule:
                    return ParseTuple(element);
                case ListRule:
                    return ParseList(element);
                case PreInstruction:
                    return ParsePreInstruction(element);
                default:
                    throw new Exception($"Tried to parse XML element '{element.Name}' as a compile rule, but the name does not "
                        + "represent a compile rule.");
            }
        }

        private static PreInstruction ParsePreInstruction(Element element)
        {
            return new PreInstruction(GetId(element),
                GetStringChild(element, DisplayName),
                GetStringChild(element, Description),
                GetStringChild(element, Opcode)
            );
        }

        private static OptionRule ParseOption(Element element)
        {
            CompileRule target = null;
            for (int i = 0; i < element.Children.Count; i++)
            {
                Element child = element.Children[i];
                CompileRule parsed = ParseCompileRule(child);
                if (parsed != null)
                    target = parsed;
            }

            return new OptionRule(GetId(element),
                GetStringChild(element, DisplayName),
                GetStringChild(element, Description),
                target,
                GetBoolChild(element, StartEnabled)
            );
        }

        private static ChoiceRule ParseChoice(Element element)
        {
            List<CompileRule> targets = new List<CompileRule>();
            for (int i = 0; i < element.Children.Count; i++)
            {
                Element child = element.Children[i];
                CompileRule parsed = ParseCompileRule(child);
                if (parsed != null)
                    targets.Add(parsed);
            }

            return new ChoiceRule(GetId(element),
                GetStringChild(element, DisplayName),
                GetStringChild(element, Description),
                targets.ToArray(),
                GetIntChild(element, StartSelected)
            );
        }

        private static TupleRule ParseTuple(Element element)
        {
            List<CompileRule> targets = new List<CompileRule>();
            for (int i = 0; i < element.Children.Count; i++)
            {
                Element child = element.Children[i];
                CompileRule parsed = ParseCompileRule(child);
                if (parsed != null)
                    targets.Add(parsed);
            }

            return new TupleRule(GetId(element),
                GetStringChild(element, DisplayName),
                GetStringChild(element, Description),
                targets.ToArray(),
                GetStringChild(element, PreviewSeparator)
            );
        }

        private static ListRule ParseList(Element element)
        {
            CompileRule target = null;
            for (int i = 0; i < element.Children.Count; i++)
            {
                Element child = element.Children[i];
                CompileRule parsed = ParseCompileRule(child);
                if (parsed != null)
                    target = parsed;
            }

            return new ListRule(GetId(element),
                GetStringChild(element, DisplayName),
                GetStringChild(element, Description),
                target,
                GetStringChild(element, AddButtonText),
                GetStringChild(element, PreviewSeparator)
            );
        }
    }
}