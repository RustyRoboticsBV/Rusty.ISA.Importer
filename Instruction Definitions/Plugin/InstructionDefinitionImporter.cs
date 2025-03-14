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
        /// Load an instruction definition from a file.
        /// </summary>
        public static InstructionDefinition Import(string filePath, Dictionary importOptions)
        {
            // Get global file & folder paths.
            string globalPath = ProjectSettings.GlobalizePath(filePath);
            string folderPath = Path.GetDirectoryName(globalPath);

            // Read file.
            string xml = File.ReadAllText(globalPath);

            // Decompile.
            try
            {
                return Deserializer.Decompile(xml, folderPath);
            }
            catch (Exception ex)
            {
                GD.PrintErr($"Could not import instruction definition at '{globalPath}' due to exception:\n{ex.Message}");
                return null;
            }
        }
    }
}