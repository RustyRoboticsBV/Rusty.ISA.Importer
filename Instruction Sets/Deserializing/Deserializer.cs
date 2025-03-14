using Godot;
using System;
using System.Text;
using Rusty.Cutscenes;
using System.Collections.Generic;

namespace Rusty.CutsceneImporter.InstructionSets
{
    /// <summary>
    /// A class that save load instruction set from a file.
    /// </summary>
    public static class Deserializer
    {
        /// <summary>
        /// Save an instruction set to a file.
        /// </summary>
        public static InstructionSet Deserialize(string filePath)
        {
            // Get global file path.
            string absolutePath = PathUtility.GetPath(filePath);

            // Open ZIP file.
            ZipReader reader = new();
            Error error = reader.Open(absolutePath);
            if (error != Error.Ok)
                throw new Exception($"Could not open file '{absolutePath}' due to error code '{error}'!");

            // Read index file.
            string index = Encoding.Default.GetString(reader.ReadFile(Keywords.IndexFilename));
            string[] files = index.Split('\n');

            // Deserialize all instructions.
            List<InstructionDefinition> definitions = new();
            for (int i = 0; i < files.Length; i++)
            {
                // Get folder and opcode.
                int slash = files[i].IndexOf('/');
                string folder = files[i].Substring(0, slash);
                string opcode = files[i].Substring(slash + 1);

                // Read XML.
                string defPath = $"{files[i]}/{Keywords.DefinitionFilename}";
                string xml = Encoding.Default.GetString(reader.ReadFile(defPath));

                // Create instruction.
                InstructionDefinition definition = InstructionDefinitions.Deserializer.Deserialize(xml, "");

                // Read icon.
                string iconPath = $"{files[i]}/{Keywords.IconFilename}.png";
                byte[] iconBytes = reader.ReadFile(iconPath);
                Image iconImage = new();
                iconImage.LoadPngFromBuffer(iconBytes);
                ImageTexture iconTexture = ImageTexture.CreateFromImage(iconImage);

                // Set icon.
                definition.Icon = iconTexture;

                // Add to list.
                definitions.Add(definition);
            }

            // Close the ZIP file.
            reader.Close();

            // Create instruction set object.
            return new(definitions.ToArray());
        }
    }
}