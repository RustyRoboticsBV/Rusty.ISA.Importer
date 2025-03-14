using Godot;
using System;

namespace Rusty.CutsceneImporter
{
    /// <summary>
    /// Can convert local paths to global paths.
    /// </summary>
    public static class PathUtility
    {
        /// <summary>
        /// Get a global file path from a resource, user or relative one.
        /// </summary>
        public static string GetPath(string path)
        {
            // Res or user.
            if (path.StartsWith("res://") && path.StartsWith("user://"))
                return ProjectSettings.GlobalizePath(path);

            // Other paths.
            string baseFolderPath = AppDomain.CurrentDomain.BaseDirectory;
            string absolutePath = "";
            if (OS.HasFeature("editor"))
                absolutePath = baseFolderPath + "..\\..\\..\\..\\" + path;
            else
                absolutePath = baseFolderPath + path;

            return absolutePath;
        }
    }
}