using System;
using System.Collections.Generic;
using Rusty.Cutscenes;
using Rusty.Csv;

namespace Rusty.CutsceneImporter.InstructionPrograms
{
    /// <summary>
    /// An deserializer for CSV-based cutscene instruction definitions.
    /// </summary>
    public static class Deserializer
    {
        /* Public methods. */
        /// <summary>
        /// Decompile a string of CSV into an instruction definition.
        /// </summary>
        public static CutsceneProgram Deserialize(CsvTable csv)
        {
            // Load instruction instances.
            List<InstructionInstance> instructions = new();
            for (int i = 0; i < csv.Height; i++)
            {
                string[] row = csv.GetRow(i);
                string[] arguments = new string[row.Length - 1];
                Array.Copy(row, 1, arguments, 0, arguments.Length);
                instructions.Add(new(row[0], arguments));
            }

            // Create program.
            return new(instructions.ToArray());
        }
    }
}