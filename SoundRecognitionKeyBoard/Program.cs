using SoundRecognitionKeyBoard.Engine;
using System;

namespace SoundRecognitionKeyBoard
{
    internal class Program
    {
        private SpeachEngine _speachEngine;

        public void Start()
        {
            _speachEngine = new SpeachEngine();
            _speachEngine.Start();
        }

        public void Stop()
        {
            _speachEngine?.Stop();
        }

        static void Main(string[] args)
        {
            var app = new Program();

            app.Start();
            Console.ReadLine();
            app.Stop();
        }
    }
}
