using System.Diagnostics;

namespace RosaryForToday.Presentation.Helpers
{
    public static class CrashLogService
    {
        public static string CrashLogPath => Path.Combine(FileSystem.AppDataDirectory, "crash.log");

        public static async Task<string?> GetCrashLogAsync()
        {
            try
            {
                var path = CrashLogPath;
                if (!File.Exists(path))
                    return null;

                return await File.ReadAllTextAsync(path).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to read crash log: {ex}");
                return null;
            }
        }

        public static void LogException(Exception? ex, string source)
        {
            var text = $"[{DateTime.UtcNow:O}] Unhandled exception ({source}): {ex}\n";
            Debug.WriteLine(text);
            try
            {
                // optional: write to local file for postmortem
                var folder = FileSystem.AppDataDirectory;
                var path = Path.Combine(folder, "crash.log");
                File.AppendAllText(path, text);
            }
            catch
            {
                // swallow to avoid throwing from the handler
            }
        }
    }
}
