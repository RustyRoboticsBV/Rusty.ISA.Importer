using Godot;
using System;
using System.Text;
using Rusty.Xml;
using Rusty.Cutscenes;
using DefinitionKeywords = Rusty.CutsceneImporter.InstructionDefinitions.Keywords;
using DefinitionSerializer = Rusty.CutsceneImporter.InstructionDefinitions.Serializer;

namespace Rusty.CutsceneImporter.InstructionSets
{
    /// <summary>
    /// A class that save an instruction set to a file.
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Save an instruction set to a file.
        /// </summary>
        public static void Serialize(InstructionSet set, string filePath)
        {
            // Get global file path.
            string absolutePath = PathUtility.GetPath(filePath);

            // Create ZIP file.
            ZipPacker packer = new ZipPacker();
            Error error = packer.Open(absolutePath);
            if (error != Error.Ok)
                throw new Exception($"Could not open file '{absolutePath}' due to error code '{error}'!");

            // Write all instructions to the file.
            for (int i = 0; i < set.Definitions.Length; i++)
            {
                InstructionDefinition def = set.Definitions[i];

                // Get opcode.
                string opcode = def.Opcode;

                // Get category.
                string category = def.Category;
                if (category == "")
                    category = Keywords.UndefinedCategory;

                // Add definition file to ZIP archive.
                packer.StartFile($"{category}/{opcode}/{Keywords.DefinitionFilename}.xml");
                Document doc = DefinitionSerializer.Serialize(def);
                try
                {
                    doc.Root.GetChild(DefinitionKeywords.Icon).InnerText = $"{Keywords.IconFilename}.png";
                }
                catch { }
                packer.WriteFile(Encoding.ASCII.GetBytes(doc.GenerateXml()));
                packer.CloseFile();

                // Add icon file to ZIP archive.
                if (def.Icon != null)
                {
                    packer.StartFile($"{category}/{opcode}/{Keywords.IconFilename}.png");
                    packer.WriteFile(def.Icon.GetImage().SavePngToBuffer());
                    packer.CloseFile();
                }
            }

            // Close the ZIP file.
            packer.Close();
        }
    }
}