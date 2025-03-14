using Godot;
using System;

namespace CutsceneImporter
{
    /// <summary>
    /// Utility class that can parse specific color names into color objects.
    /// </summary>
    public static class ColorNameParser
    {
        public static Color Parse(string colorName)
        {
            switch (colorName)
            {
                case "white":
                    return Color.FromHtml("FFFFFFFF");
                case "gray":
                case "grey":
                    return Color.FromHtml("808080FF");
                case "black":
                    return Color.FromHtml("000000FF");
                case "red":
                    return Color.FromHtml("FF0000FF");
                case "orange":
                    return Color.FromHtml("FF8000FF");
                case "yellow":
                    return Color.FromHtml("FFFF00FF");
                case "lime":
                case "chartreuse":
                    return Color.FromHtml("80FF00FF");
                case "green":
                    return Color.FromHtml("00FF00FF");
                case "mint":
                case "spring_green":
                    return Color.FromHtml("00FF80FF");
                case "cyan":
                    return Color.FromHtml("00FFFFFF");
                case "azure":
                    return Color.FromHtml("0080FFFF");
                case "blue":
                    return Color.FromHtml("0000FFFF");
                case "violet":
                    return Color.FromHtml("8000FFFF");
                case "magenta":
                    return Color.FromHtml("FF00FFFF");
                case "rose":
                    return Color.FromHtml("FF0080FF");
                case "brown":
                    return Color.FromHtml("804000FF");
                case "purple":
                    return Color.FromHtml("C000FFFF");
                case "pink":
                    return Color.FromHtml("FFBFDFFF");
                case "clear":
                case "transparent":
                    return Color.FromHtml("00000000");
                default:
                    throw new ArgumentException($"The color name '{colorName}' does not correspond to a valid color.");
            }
        }
    }
}
