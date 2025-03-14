using Godot;

namespace Rusty.CutsceneImporter
{
    /// <summary>
    /// Utility class that can serializer colors.
    /// </summary>
    public static class ColorSerializer
    {
        /// <summary>
        /// Serialize a color.
        /// </summary>
        public static string Serialize(Color color)
        {
            return $"#{color.ToHtml(true)}";
        }
    }
}
