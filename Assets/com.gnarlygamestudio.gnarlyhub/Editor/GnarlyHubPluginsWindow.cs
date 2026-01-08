#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace Gnarly.Hub.Editor
{
    /// <summary>
    /// Editor window for managing optional Gnarly Hub plugins.
    /// </summary>
    public class GnarlyHubPluginsWindow : EditorWindow
    {
        private const string RemoteJsonUrl = "https://gnarlyteam.com/OptionalPlugins.json";
        private bool _isRefreshing;

        private Vector2 _scrollPosition;
        private PluginContainer _pluginContainer;

        [MenuItem("Gnarly Hub/Plugins", priority = 10)]
        public static void ShowWindow()
        {
            var window = GetWindow<GnarlyHubPluginsWindow>("Gnarly Hub Plugins");
            window.minSize = new Vector2(500, 300);
            window.DownloadAndUpdatePluginsAsync();
            window.Show();
        }

        private void OnGUI()
        {
            var titleStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 22,
                normal = { textColor = new Color(0.85f, 0.85f, 0.85f) }
            };

            GUILayout.Space(10);
            EditorGUILayout.LabelField("🧩 Gnarly Plugins", titleStyle, GUILayout.Height(30));
            GUILayout.Space(12);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(8);

            if (_isRefreshing)
            {
                GUILayout.Label("Refreshing...", EditorStyles.boldLabel);
                return;
            }

            GUILayout.Space(10);

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
            var plugins = GnarlyPluginParser.ParsePlugins(_pluginContainer);
            foreach (var plugin in plugins)
            {
                DrawPluginRow(plugin);
                GUILayout.Space(4);
            }

            EditorGUILayout.EndScrollView();
        }

        /// <summary>
        /// Draws a single plugin entry row.
        /// </summary>
        private async Task DownloadAndUpdatePluginsAsync()
        {
            _isRefreshing = true;
            Repaint();

            try
            {
                using (var www = new UnityWebRequest(RemoteJsonUrl))
                {
                    www.downloadHandler = new DownloadHandlerBuffer();
                    var operation = www.SendWebRequest();

                    while (!operation.isDone)
                    {
                        await Task.Yield();
                    }

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"❌ Failed to download plugins: {www.error}");
                        EditorUtility.DisplayDialog("Error", $"Failed to download plugins: {www.error}", "OK");
                        return;
                    }


                    var pluginJson = www.downloadHandler.text;
                    _pluginContainer = JsonUtility.FromJson<PluginContainer>(pluginJson);

                    Debug.Log("✅ Successfully updated plugins list");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"❌ Error downloading plugins: {ex.Message}");
                EditorUtility.DisplayDialog("Error", $"Error downloading plugins: {ex.Message}", "OK");
            }
            finally
            {
                _isRefreshing = false;
                Repaint();
            }
        }

        /// <summary>
        /// Draws a single plugin entry row.
        /// </summary>
        /// <param name="plugin">Plugin info to display.</param>
        private void DrawPluginRow(Plugin plugin)
        {
            var packageName = plugin.CustomPackageName == string.Empty ? plugin.Registry : plugin.CustomPackageName;
            var packageInfo = PackageInfo.FindForPackageName(packageName);
            var isInstalled = packageInfo != null;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    var nameStyle = new GUIStyle(EditorStyles.label)
                    {
                        fontSize = 15,
                        fontStyle = FontStyle.Bold,
                        normal = { textColor = Color.white }
                    };

                    GUILayout.Label(plugin.DisplayName, nameStyle, GUILayout.ExpandWidth(true));
                    GUILayout.FlexibleSpace();

                    if (isInstalled)
                    {
                        DrawBadge($"v{packageInfo.version}", new Color(0.27f, 0.62f, 0.27f));
                    }
                    else
                    {
                        DrawBadge("Not Installed", new Color(0.9f, 0.25f, 0.25f));
                    }
                }

                GUILayout.Space(4);

                using (new EditorGUILayout.HorizontalScope())
                {
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Source", GUILayout.Width(80), GUILayout.Height(24)))
                    {
                        ShowSource(plugin);
                    }

                    if (!isInstalled)
                    {
                        if (GUILayout.Button("Install", GUILayout.Width(80), GUILayout.Height(24)))
                        {
                            InstallPlugin(plugin);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button("Update", GUILayout.Width(80), GUILayout.Height(24)))
                        {
                            UpdatePlugin(plugin);
                        }

                        if (GUILayout.Button("Remove", GUILayout.Width(80), GUILayout.Height(24)))
                        {
                            RemovePlugin(plugin);
                        }
                    }
                }

                GUILayout.Space(4);
            }
        }

        /// <summary>
        /// Draws a small-colored badge (status label).
        /// </summary>
        /// <param name="text">Badge text.</param>
        /// <param name="color">Background color.</param>
        private void DrawBadge(string text, Color color)
        {
            var badgeStyle = new GUIStyle(EditorStyles.miniLabel)
            {
                normal = { textColor = Color.white },
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                padding = new RectOffset(6, 6, 2, 2),
                margin = new RectOffset(2, 2, 0, 0)
            };

            var badgeRect = GUILayoutUtility.GetRect(new GUIContent(text), badgeStyle, GUILayout.ExpandWidth(false));
            EditorGUI.DrawRect(badgeRect, color);
            EditorGUI.LabelField(badgeRect, text, badgeStyle);
        }

        private void ShowSource(Plugin plugin)
        {
            switch (plugin.Source)
            {
                case PluginSource.OpenUpm:
                {
                    Application.OpenURL($"https://openupm.com/packages/{plugin.Registry}");
                    break;
                }
                case PluginSource.Git:
                {
                    Application.OpenURL(plugin.Registry);
                    break;
                }
            }
        }

        private void InstallPlugin(Plugin plugin)
        {
            switch (plugin.Source)
            {
                case PluginSource.OpenUpm:
                {
                    RunOpenUpmCommand($"add {plugin.Registry}");
                    break;
                }
                case PluginSource.Git:
                {
                    Client.Add(plugin.Registry);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            EditorUtility.RequestScriptReload();
        }

        private void UpdatePlugin(Plugin plugin)
        {
            switch (plugin.Source)
            {
                case PluginSource.OpenUpm:
                {
                    RunOpenUpmCommand($"add {plugin.Registry}");
                    break;
                }
                case PluginSource.Git:
                {
                    Client.Add(plugin.Registry);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            EditorUtility.RequestScriptReload();
        }

        private void RemovePlugin(Plugin plugin)
        {
            switch (plugin.Source)
            {
                case PluginSource.OpenUpm:
                {
                    RunOpenUpmCommand($"remove {plugin.Registry}");
                    break;
                }
                case PluginSource.Git:
                {
                    var packageName = plugin.CustomPackageName == string.Empty
                        ? plugin.Registry
                        : plugin.CustomPackageName;
                    Client.Remove(packageName);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            EditorUtility.RequestScriptReload();
        }

        /// <summary>
        /// Runs an OpenUPM CLI command safely.
        /// </summary>
        /// <param name="args">Command arguments (e.g., "add" or "remove").</param>
        private void RunOpenUpmCommand(string args)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = CreateProcessStartInfo("openupm", args)
                };

                process.Start();
                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(output)) Debug.Log(output);
                if (!string.IsNullOrWhiteSpace(error)) Debug.LogWarning($"⚠️ {error}");
            }
            catch (Exception e)
            {
                Debug.LogError("❌ Failed to run openupm: ");
                Debug.LogException(e);
            }
        }

        private ProcessStartInfo CreateProcessStartInfo(string command, string args)
        {
            var startInfo = new ProcessStartInfo
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var combinedArgs = string.IsNullOrWhiteSpace(args) ? command : $"{command} {args}";
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/c \"{combinedArgs}\"";
            }
            else
            {
                startInfo.FileName = command;
                startInfo.Arguments = args;
            }

            return startInfo;
        }
    }
}
#endif