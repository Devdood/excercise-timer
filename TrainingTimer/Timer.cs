using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TrainingTimer
{
    public class Timer
    {
        private Thread timerThread;

        public void Start()
        {
            timerThread = new Thread(new ThreadStart(Loop));
            timerThread.Start();
        }

        private void Loop()
        {
            while (true)
            {
                int round = 0;
                Console.Clear();
                Console.Write("Enter amount of rounds:");
                string rounds = Console.ReadLine();
                Console.Write("Enter excercise hold time (sec > 6):");
                string hold = Console.ReadLine();
                Console.Write("Enter excercise pause time (sec):");
                string pause = Console.ReadLine();
                if (int.TryParse(rounds, out int r) && int.TryParse(hold, out int h) && h > 3 && int.TryParse(pause, out int p))
                {
                    while (round < r)
                    {
                        round += 1;
                        StartRound(round, h, p);
                    }
                }
            }
        }

        private void StartRound(int round, int hold, int pause)
        {
            int half = (hold / 2) * 1000;

            Console.Clear();
            Console.WriteLine("Round: " + round);
            PlayBeep(BeepLevel.MEDIUM);
            Console.WriteLine("Prepare for training 5 sec!");
            Thread.Sleep(2000);
            CountThree();
            PlayBeep(BeepLevel.MEDIUM);
            Console.WriteLine(string.Format("Hold it for {0} sec", hold));
            Thread.Sleep(half);
            Console.WriteLine(string.Format("{0} seconds left!", half / 1000));
            Thread.Sleep(half - 3000);
            CountThree();
            PlayBeep(BeepLevel.MEDIUM);
            Console.WriteLine(string.Format("{0} sec break. Feel free to rest.", pause));
            Thread.Sleep(pause * 1000);
        }

        private static void CountThree()
        {
            PlayBeep(BeepLevel.LOW);
            Console.Write("3...");
            Thread.Sleep(1000);
            PlayBeep(BeepLevel.LOW);
            Console.Write("2...");
            Thread.Sleep(1000);
            PlayBeep(BeepLevel.LOW);
            Console.WriteLine("1...");
            Thread.Sleep(1000);
        }

        private static void PlayBeep(BeepLevel beepLevel)
        {
            switch(beepLevel)
            {
                case BeepLevel.LOW:
                    Console.Beep(200, 200);
                    break;
                case BeepLevel.MEDIUM:
                    Console.Beep(400, 400);
                    break;
                default:
                    break;
            }
        }
    }
}