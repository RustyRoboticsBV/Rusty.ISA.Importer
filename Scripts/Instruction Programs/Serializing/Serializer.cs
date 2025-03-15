using Rusty.Cutscenes;
using Rusty.Csv;
using System.Collections.Generic;

namespace Rusty.CutsceneImporter.InstructionPrograms
{
    /// <summary>
    /// A class that can serialize an instruction definition into an CSV document.
    /// </summary>
    public static class Serializer
    {
        /* Public methods. */
        /// <summary>
        /// Convert an instruction definition into a CSV table.
        /// </summary>
        public static CsvTable Serialize(CutsceneProgram program)
        {
            List<string> cells = new();

            // Figure out maximum number of arguments.
            int maxArgs = 0;
            for (int i = 0; i < program.Length; i++)
            {
                if (program[i].Arguments.Length > maxArgs)
                    maxArgs = program[i].Arguments.Length;
            }

            // Create cells.
            for (int i = 0; i < program.Length; i++)
            {
                // Add opcode.
                cells.Add(ProcessCell(program[i].Opcode));

                // Add arguments.
                for (int j = 0; j < program[i].Arguments.Length; j++)
                {
                    cells.Add(ProcessCell(program[i].Arguments[j]));
                }

                // Add empty cells to create a rectangular table.
                for (int j = program[i].Arguments.Length; j < maxArgs; j++)
                    cells.Add("");
            }

            // Return table.
            return new(program.ResourceName, cells.ToArray(), maxArgs + 1);
        }

        /* Private methods. */
        /// <summary>
        /// Alter a cell string to properly handle line-breaks.
        /// </summary>
        private static string ProcessCell(string cell)
        {
            return cell.Replace("\\n", "\\\\n").Replace("\n", "\\n");
        }
    }
}