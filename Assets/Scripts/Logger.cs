using UnityEngine;

public static class Logger
{
    public enum LogLevel { None, Error, Warn, Info }
    private static LogLevel _logLevel = LogLevel.Info;

    public static void SetLogLevel(LogLevel level) => _logLevel = level;

    public static void Error(string message)
    {
        if (_logLevel >= LogLevel.Error)
            Debug.LogError($"[ERROR] {message}");
    }

    public static void Warn(string message)
    {
        if (_logLevel >= LogLevel.Warn)
            Debug.LogWarning($"[WARN] {message}");
    }

    public static void Info(string message)
    {
        if (_logLevel >= LogLevel.Info)
            Debug.Log($"[INFO] {message}");
    }
}