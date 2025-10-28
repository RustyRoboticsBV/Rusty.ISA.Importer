using Godot;
using System;
using System.Collections.Generic;
using Rusty.Csv;

namespace Rusty.ISA.Importer;

[GlobalClass]
public partial class ProgramImporter : Node
{
    public class Node
    {
        public string Opcode { get; set; } = "";
        public List<string> Arguments { get; } = new();
        public List<Node> Children { get; } = new();

        public Node(CsvTable table, ref int currentRow)
        {
            Opcode = table[0, currentRow];
            for (int i = 1; i < table.Width; i++)
            {
                Arguments.Add(table[i, currentRow]);
            }

            currentRow++;

            switch (Opcode)
            {
                // Groups.
                case "PRO":
                case "MTA":
                case "ISA":
                case "DEF":
                case "RUL":
                case "DIC":
                case "GRA":
                case "CMT":
                case "FRM":
                case "JNT":
                case "NOD":
                case "INS":
                case "PRE":
                case "PST":
                case "OPT":
                case "CHO":
                case "TPL":
                case "LST":
                case "GTG":
                case "ENG":
                    while (currentRow < table.Height)
                    {
                        Node child = new(table, ref currentRow);
                        Children.Add(child);
                        if (child.Opcode == "EOG")
                            break;
                    }
                    break;

                // Non-groups.
                default:
                    break;
            }
        }

        public Node GetChild(string opcode)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].Opcode == opcode)
                    return Children[i];
            }
            return null;
        }
    }

    public class Definition
    {
        public string Opcode { get; set; } = "";
        public int ParameterCount { get; set; } = 0;
        public bool IsEditorOnly { get; set; }
    }

    public class InstructionSet
    {
        public Dictionary<string, Definition> Definitions { get; } = new();

        public InstructionSet(Node root)
        {
            foreach (Node child in root.Children)
            {
                Definition definition = Process(child);
                if (definition == null)
                    continue;

                Definitions.Add(definition.Opcode, definition);
            }
        }

        private Definition Process(Node node)
        {
            if (node.Opcode != "DEF")
                return null;

            Definition definition = new();
            definition.Opcode = node.Arguments[0];
            definition.IsEditorOnly = bool.Parse(node.Arguments[1]);
            foreach (Node child in node.Children)
            {
                if (child.Opcode == "PAR")
                    definition.ParameterCount++;
            }

            return definition;
        }
    }

    public static Program Import(string name, string csv, bool stripEditorOnlies)
    {
        // Load CSV table.
        CsvTable table = new(name, csv);
        if (table.Width == 0 || table.Height == 0)
            throw new Exception("Bad table.");

        // Build node tree.
        int currentRow = 0;
        Node root = new(table, ref currentRow);

        // Build instruction set.
        InstructionSet isa = new(root?.GetChild("MTA")?.GetChild("ISA"));
        if (isa == null)
            throw new NullReferenceException("Bad form: program has not ISA metadata block!");

        // Build instructions.
        List<InstructionInstance> instructions = new();
        for (int i = 0; i < table.Height; i++)
        {
            string opcode = table[0, i];
            Definition definition = isa.Definitions[opcode];
            if (stripEditorOnlies && definition.IsEditorOnly)
                continue;

            string[] arguments = new string[definition.ParameterCount];
            for (int j = 0; j < arguments.Length && j < table.Width; j++)
            {
                arguments[j] = table[j + 1, i];
            }
            instructions.Add(new(opcode, arguments));
        }

        // Build program.
        return new(name, instructions.ToArray());
    }
}
