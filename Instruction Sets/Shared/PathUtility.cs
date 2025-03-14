using Godot;
using System;

namespace Rusty.CutsceneImporter.InstructionSets
{
    /// <summary>
    /// Can convert local paths to global paths.
    /// </summary>
    public static class PathUtility
    {
        /// <summary>
        /// Get a global file path from a relative one.
        /// </summary>
        public static string GetPath(string relativePath)
        {
            string baseFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = "";
            if (OS.HasFeature("editor"))
                absolutePath = baseFolderPath + "..\\..\\..\\..\\" + relativePath;
            else
                absolutePath = baseFolderPath + relativePath;

            return absolutePath;
        }
    }
}