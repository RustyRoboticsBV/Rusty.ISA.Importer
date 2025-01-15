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
        public List<ParameterDefinition> parameters = new List<ParameterDefinition>();

        // Runtime.
        public string implementation = "";

        // Meta-data.
        public Texture2D icon = null;
        public string displayName = "";
        public string description = "";
        public string category = "";

        // Editor.
        public EditorNodeInfo editorNodeInfo = null;
        public bool hideDefaultOutput = false;
        public List<PreviewTerm> previewTerms = new List<PreviewTerm>();
        public List<CompileRule> compileRules = new List<CompileRule>();
    }
}