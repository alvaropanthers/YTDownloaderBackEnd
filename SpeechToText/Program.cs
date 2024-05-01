using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

//Environment.SetEnvironmentVariable("SPEECH_KEY", "259bce52ea7a4710b2c009a90e9bf571");
//Environment.SetEnvironmentVariable("SPEECH_REGION", "eastus");

//var environmentVariables = Environment.GetEnvironmentVariables();
//foreach (var variable in environmentVariables)
//{
//    Console.WriteLine(variable.ToString());
//}

string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");

void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
{
    switch (speechRecognitionResult.Reason)
    {
        case ResultReason.RecognizedSpeech:
            Console.WriteLine($"RECOGNIZED: Text={speechRecognitionResult.Text}");
            break;
        case ResultReason.NoMatch:
            Console.WriteLine($"NOMATCH: Speech could not be recognized.");
            break;
        case ResultReason.Canceled:
            var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

            if (cancellation.Reason == CancellationReason.Error)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                Console.ForegroundColor = ConsoleColor.White;
            }
            break;
    }
}

//var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
var speechConfig = SpeechConfig.FromSubscription("259bce52ea7a4710b2c009a90e9bf571", "eastus");
speechConfig.SpeechRecognitionLanguage = "en-US";

using var audioConfig = AudioConfig.FromWavFileInput("C:\\Users\\Alvaro.Orozco\\Documents\\music_pp\\A286GQRWPU.wav");
using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
OutputSpeechRecognitionResult(speechRecognitionResult);