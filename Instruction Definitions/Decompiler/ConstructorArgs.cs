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
        public List<Parameter> parameters = new();
        public Implementation implementation = null;

        // Meta-data.
        public Texture2D icon = null;
        public string displayName = "";
        public string description = "";
        public string category = "";

        // Editor.
        public EditorNodeInfo editorNodeInfo = null;
        public string previewSeparator = "";
        public List<PreviewTerm> previewTerms = new();
        public List<CompileRule> preInstructions = new();
        public List<CompileRule> postInstructions = new();
    }
}