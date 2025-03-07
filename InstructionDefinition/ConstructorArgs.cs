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
        public List<Parameter> parameters = new List<Parameter>();
        public Implementation implementation = null;

        // Meta-data.
        public Texture2D icon = null;
        public string displayName = "";
        public string description = "";
        public string category = "";

        // Editor.
        public EditorNodeInfo editorNodeInfo = null;
        public List<PreviewTerm> previewTerms = new List<PreviewTerm>();
        public List<CompileRule> preInstructions = new List<CompileRule>();
        public List<CompileRule> postInstructions = new List<CompileRule>();
    }
}