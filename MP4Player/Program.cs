using MP4Player;
using System.Reflection.Metadata;
using System.IO;
using System.Text;
using PPFileLibrary;
using CSCore;
using System.Globalization;
using CSCore.Codecs;
using CSCore.Streams.Effects;
using CSCore.Codecs.WAV;

Console.ForegroundColor = ConsoleColor.White;

const string FILE_EXTENSION = ".pp";
const string MF = "C:\\Users\\Alvaro.Orozco\\Documents\\music_pp";
var audioPlayer = new AudioPlayer();
List<PPFile>? ppFiles;
PPFile? currentPPFile = null;
bool executing = true;
bool isLoop = false;

void Log(object log) 
{
    Console.Write(log);
}

void LoadFiles()
{
    ppFiles = new List<PPFile>();
    foreach (var file in Directory.GetFiles(MF))
    {
        var fileName = Path.GetFileName(file);
        if (fileName.Contains(FILE_EXTENSION))
        {
            var result = File.OpenRead(file);
            var buffer = new byte[result.Length];
            result.Read(buffer, 0, buffer.Length);

            var decodedString = Encoding.Default.GetString(buffer);

            var ppFile = new PPFile(decodedString);
            ppFiles.Add(ppFile);
        }
    }
}

void ShowFilesOptions()
{
    if (ppFiles == null) return;

    foreach(var ppFile in ppFiles)
    {
        Log($"id=");
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Log($"{ppFile.Id}\n");
        Console.ForegroundColor = ConsoleColor.White;
        Log($"name={ppFile.Title}\n");
        Log("\n");
    }
}

void PlayFile(string id)
{
    var ppFile = ppFiles.Where(s => s.Id == id).First();
    currentPPFile = ppFile;
    audioPlayer.PlayAudioFile($"{MF}\\{ppFile.FileName}");
}

void ResumeFile()
{
    audioPlayer.ResumeAudioFile();
}

void PauseFile()
{
    audioPlayer.PauseAudioFile();
}

void StopFile()
{
    currentPPFile = null;
    audioPlayer.StopAudioFile();
}

static TimeSpan StringToTimeSpan(string str)
{
    return TimeSpan.ParseExact(str, "hh:mm:ss", CultureInfo.InvariantCulture);
}

void SetAudioFileToPercentage(int percentage) 
{
    var waveSource = audioPlayer.GetWave();
    var calculation = ((double)percentage / 100) * waveSource.Length;
    int finalVal = (int)calculation;
    waveSource.SetPosition(new TimeSpan(0, 0, finalVal));
}

void ShowPrompt()
{
    Log("Enter id of file to play: \n");
    Console.ForegroundColor = ConsoleColor.Green;
    string result = Console.ReadLine();
    Console.ForegroundColor = ConsoleColor.White;

    if (result == null) return;

    switch (result.ToLower())
    {
        case "e":
            StopFile();
            executing = false;
            return;

        case "p":
            PauseFile();
            return;

        case "r":
            ResumeFile();
            return;

        case "t":
            StopFile();
            return;

        case "h":
            Log("Enter %: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string rp = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            SetAudioFileToPercentage(int.Parse(rp));
            return;

        case "a":
            LoadFiles();
            return;
    }

    PlayFile(result);
}

LoadFiles();

while(executing)
{
    ShowFilesOptions();

    Console.ForegroundColor = ConsoleColor.Cyan;
    Log("p - pause. r - resume. t - stops playing file. h - sets position. e - exits program. a - reloads list\n");
    Log("\n");
    Console.ForegroundColor = ConsoleColor.White;

    if (currentPPFile != null)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Log($"Current playing: {currentPPFile.Title}\n");

        var currentState = audioPlayer.GetCurrentState();
        if (currentState != null) 
        {
            var waveSource = audioPlayer.GetWave();
            var currentPercentage = Math.Ceiling((double)waveSource.Position / (double)waveSource.Length * 100);
            Log($"Played: {currentPercentage}%, Time: {waveSource.GetTime(waveSource.Length)}, Format: {waveSource.WaveFormat.WaveFormatTag}\n");
            Log($"State: {currentState.Value}\n");
        }

        Console.ForegroundColor = ConsoleColor.White;
        Log("\n");
    }

    ShowPrompt();
    Log("\n");
}