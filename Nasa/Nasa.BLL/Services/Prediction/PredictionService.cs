using Nasa.Common.DTO.Coordinates;

namespace Nasa.BLL.Services.Prediction;

public class PredictionService
{
    public void Predict(IEnumerable<CoordinatesDto> coordinates)
    {
        // var assemblyPath = Directory.GetCurrentDirectory().Split('\\').SkipLast(1);
        // var filePath = string.Join("\\", assemblyPath) + "\\Nasa.BLL\\Services\\Prediction\\unt.py";
        //
        // string argsFile = $"{Path.GetDirectoryName(filePath)}\\{Guid.NewGuid()}.txt";
        //
        // ProcessStartInfo startInfo = new ProcessStartInfo
        // {
        //     FileName = "C:\\Users\\vlada\\AppData\\Local\\Programs\\Python\\Python311\\python.exe",
        //     UseShellExecute = false,
        //     RedirectStandardOutput = true,
        //     CreateNoWindow = false,
        //     RedirectStandardError = true
        // };
        //
        // var result = string.Empty;
        //
        // try
        // {
        //     using var sw = new StreamWriter(argsFile);
        //
        //     int count = 0;
        //     foreach (var coordinate in coordinates)
        //     {
        //         sw.WriteLine(coordinate.ToString());
        //         count++;
        //
        //         if (count >= 150)
        //         {
        //             break;
        //         }
        //     }
        //     
        //     startInfo.Arguments = string.Format(
        //         "{0} {1}", string.Format(@"""{0}""", filePath), string.Format(@"""{0}""", argsFile));
        //     
        //     PythonExecutor.ExecuteScript(startInfo, out result);
        // }
        // finally
        // {
        //     File.Delete(argsFile);
        // }
    }
}