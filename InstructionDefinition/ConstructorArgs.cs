using Godot;
using System.Collections.Generic;
using Rusty.Cutscenes;

namespace Rusty.CutsceneImporter.InstructionDefinitions
{
    /// <summary>
    /// A set of instruction definition constructor argumemts.
    /// </summary>
    internal class ConstructorArgs
    {
        // Definition.
        public string opcode = "MISSING_OPCODE";
        public List<ParameterDefinition> parameters = new();

        // Runtime.
        public string implementation = "";

        // Meta-data.
        public Texture2D icon;
        public string displayName = "";
        public string description = "";
        public string category = "";

        // Editor.
        public EditorNodeInfo editorNodeInfo;
        public bool hideDefaultOutput;
        public List<PreviewTerm> previewTerms = new();
        public List<CompileRule> compileRules = new();
    }
}