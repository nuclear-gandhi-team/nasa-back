using System.Diagnostics;

namespace Nasa.BLL.Services.Prediction;

public static class PythonExecutor
{
    public static void ExecuteScript(ProcessStartInfo startInfo, out string result)
    {
        using var process = Process.Start(startInfo)!;
        using var reader = process.StandardOutput;
        var errors = process.StandardError.ReadToEnd();
        if (errors.Any())
        {
            throw new Exception(errors);
        }
        result = reader.ReadToEnd();
        process.WaitForExit();
    }
}