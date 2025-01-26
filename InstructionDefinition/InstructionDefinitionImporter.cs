using Godot;
using Godot.Collections;
using System;
using System.IO;
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
        /* Public methods. */
        /// <summary>
        /// Load an instruction definition from some file path.
        /// </summary>
        public static InstructionDefinition Import(string xmlFilePath, Dictionary importOptions)
        {
            // Get global file & folder paths.
            string filePath = ProjectSettings.GlobalizePath(xmlFilePath);
            string folderPath = Path.GetDirectoryName(xmlFilePath);
            string xml = File.ReadAllText(xmlFilePath);
            try
            {
                return InstructionDefinitionDecompiler.Decompile(xml, folderPath);
            }
            catch (Exception ex)
            {
                string filename = Path.GetFileName(xmlFilePath);
                throw new Exception($"Instrtuction definition '{xmlFilePath}': {ex.Message}");
            }
        }
    }
}