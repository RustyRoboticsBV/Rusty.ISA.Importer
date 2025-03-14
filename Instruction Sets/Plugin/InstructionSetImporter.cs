using Godot;
using Godot.Collections;
using System;
using System.IO;
using Rusty.Cutscenes;

namespace Rusty.CutsceneImporter.InstructionSets
{
    /// <summary>
    /// An importer for XML-based cutscene instruction definitions.
    /// </summary>
    [Tool]
    [GlobalClass]
    public partial class InstructionSetImporter : Node
    {
        /* Public methods. */
        /// <summary>
        /// Load an instruction set from a file.
        /// </summary>
        public static InstructionSet Import(string filePath, Dictionary importOptions)
        {
            // Decompile.
            try
            {
                return Deserializer.Deserialize(filePath);
            }
            catch (Exception ex)
            {
                GD.PrintErr($"Could not import instruction set at '{filePath}' due to exception:\n{ex.Message}");
                return null;
            }
        }
    }
}