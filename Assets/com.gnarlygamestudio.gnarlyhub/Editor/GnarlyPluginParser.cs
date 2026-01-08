#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;

namespace Gnarly.Hub.Editor
{
    /// <summary>
    /// Defines the plugin source type.
    /// </summary>
    public enum PluginSource
    {
        Git,
        OpenUpm
    }

    /// <summary>
    /// Holds parsed plugin data.
    /// </summary>
    public class Plugin
    {
        public string Registry;
        public string CustomPackageName;
        public string DisplayName;
        public PluginSource Source;
    }

    /// <summary>
    /// Template model for JSON parsing.
    /// </summary>
    [Serializable]
    public class PluginTemplate
    {
        public string registry;
        public string customPackageName = string.Empty;
        public string displayName;
        public string source;
    }

    /// <summary>
    /// Container for multiple plugin templates.
    /// </summary>
    [Serializable]
    public class PluginContainer
    {
        public PluginTemplate[] pluginTemplates;
    }

    /// <summary>
    /// Provides helper methods for parsing plugin JSON files.
    /// </summary>
    public static class GnarlyPluginParser
    {
        /// <summary>
        /// Reads and parses plugin data from the specified container.
        /// </summary>
        /// <param name="pluginContainer">The data-driven container holding the raw plugin templates to be processed.</param>
        /// <returns>An array of parsed Plugin objects, or null if the container is empty or invalid.</returns>
        public static Plugin[] ParsePlugins(PluginContainer pluginContainer)
        {
            var pluginTemplates = pluginContainer.pluginTemplates;

            if (pluginTemplates == null || pluginTemplates.Any() == false)
            {
                Debug.LogWarning("⚠️ No plugins found in JSON.");
                return null;
            }

            var plugins = new Plugin[pluginTemplates.Length];
            for (var i = 0; i < pluginTemplates.Length; i++)
            {
                var template = pluginTemplates[i];
                var plugin = new Plugin
                {
                    Registry = template.registry,
                    DisplayName = template.displayName,
                    CustomPackageName = template.customPackageName,
                };

                try
                {
                    Enum.TryParse(typeof(PluginSource), template.source, true, out var source);
                    plugin.Source = (PluginSource)source;
                }
                catch (Exception sourceParseException)
                {
                    Debug.LogError(sourceParseException);
                    throw;
                }

                plugins[i] = plugin;
            }

            return plugins;
        }
    }
}
#endif