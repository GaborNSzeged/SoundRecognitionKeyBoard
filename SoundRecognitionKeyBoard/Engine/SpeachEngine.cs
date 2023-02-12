using System;
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;

namespace SoundRecognitionKeyBoard.Engine
{
    internal class SpeachEngine
    {
        private const string CommandLeft = "right";
        private const string CommandRight = "left";
        private const string CommandGoLeft = "go right";
        private const string CommandGoRight = "go left";
        private const string CommandStop = "stop";

        private SpeechRecognitionEngine _recognizer;
        private bool _pressed;
        private int _delayBetweenPress = 200;
        private int _delayBeforeStartPressing;

        public SpeachEngine()
        {
            _delayBeforeStartPressing = _delayBetweenPress + 100;
        }

        internal void Start()
        {
            _recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            Choices choices = new Choices();
            choices.Add(CommandRight);
            choices.Add(CommandLeft);
            choices.Add(CommandGoLeft);
            choices.Add(CommandGoRight);
            choices.Add(CommandStop);

            var grammer = new Grammar(new GrammarBuilder(choices));
            _recognizer.LoadGrammar(grammer);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(HandleSpeechRecognized);
        }

        ~SpeachEngine()
        {
            Stop();
        }

        internal void Stop()
        {
            _recognizer?.Dispose();
            _recognizer = null;
        }

        private void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string command = e.Result.Text;
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}: " + command);

            Task.Run(() =>
            {
                if (command == CommandLeft)
                {
                    _pressed = false;
                    KeyboardCommandDelegator.KeyDown(65);
                    KeyboardCommandDelegator.KeyUp(65);
                }
                else if (command == CommandRight)
                {
                    _pressed = false;
                    KeyboardCommandDelegator.KeyDown(68);
                    KeyboardCommandDelegator.KeyUp(68);
                }
                else if (command == CommandGoLeft)
                {
                    _pressed = false;
                    Thread.Sleep(_delayBeforeStartPressing);
                    _pressed = true;

                    while (_pressed)
                    {
                        KeyboardCommandDelegator.KeyDown(65);
                        KeyboardCommandDelegator.KeyUp(65);
                        Thread.Sleep(_delayBetweenPress);
                    }
                }
                else if (command == CommandGoRight)
                {
                    _pressed = false;
                    Thread.Sleep(_delayBeforeStartPressing);
                    _pressed = true;

                    while (_pressed)
                    {
                        KeyboardCommandDelegator.KeyDown(68);
                        KeyboardCommandDelegator.KeyUp(68);
                        Thread.Sleep(_delayBetweenPress);
                    }
                }
                else if (command == CommandStop)
                {
                    _pressed = false;
                }
            });
        }
    }
}
