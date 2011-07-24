using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;

namespace DelayStart
{
    class Program
    {
        static Timer _timer;
        static string _program;
        static string _arguments;

        [STAThread]
        static void Main(string[] args)
        {
            if (!CheckArguments(args))
            {
                WriteL("Delays the start of a program\n");
                WriteL("DELAYSTART time program [arguments]\n");
                WriteL("  time          The amount of time to wait before running the program (in seconds).");
                WriteL("  program       The program to run after the delay.");
                WriteL("  arguments     Optional arguments to be passed to the application.");
                return;
            }
            _program = args[1].Trim();
            if (args.Length == 3)
                _arguments = args[2].Trim();
            double delay;
            double.TryParse(args[0].Trim(), out delay);
            _timer = new Timer(delay * 1000);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;
            while (_timer.Enabled)
                System.Threading.Thread.Sleep(1000);
        }

        static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo(_program, _arguments);
            psi.WindowStyle = ProcessWindowStyle.Normal;
            psi.UseShellExecute = false;
            Process.Start(psi);
            _timer.Enabled = false;
        }

        static bool CheckArguments(string[] args)
        {
            if ((args.Length != 2) && (args.Length != 3))
                return false;
            double n;
            if (!double.TryParse(args[0].Trim(), out n))
                return false;
            return true;
        }

        static void WriteL(string line)
        {
            Console.WriteLine(line);
        }
    }
}
