using Common;
using InputNS;
using LoggerSpace;
using Modul1;
using Module2;
using ReaderNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlConsole
{
    public class SignalSystem
    {
        private IInput inputProxy;
        private IReader readingProxy;
        private static SignalSystem singletonSystemInstance;

        public IInput InputProxy { get => inputProxy; }
        public IReader ReadingProxy { get => readingProxy;}

        private SignalSystem()
        {
            InitSystem();
        }


        private void InitSystem()
        {
            ILogging module1Logger = new Logger("module1_log.txt");
            ILogging module2Logger = new Logger("module2_log.txt");
            ILogging inputLogger = new Logger("input_log.txt");
            ILogging readerLogger = new Logger("reader_log.txt");

            Module2ServiceProvider module2 = new Module2ServiceProvider(module2Logger);
            Module1ServiceProvider module1 = new Module1ServiceProvider(module1Logger, module2);
            Input input = new Input(inputLogger, module2, module1);
            Reader reader = new Reader(readerLogger, module2);
            input.StartDataFlow();
            inputProxy = input;
            readingProxy = reader;
        }


        public static SignalSystem GetOrCreateSystem()
        {
            if (singletonSystemInstance == null)
            {
                singletonSystemInstance = new SignalSystem();
            }

            return singletonSystemInstance;
        }


        private void InteractForHistoryWriting()
        {
            WriteSignalMenu();
            int signalCode = GetSignalCode();
            if (signalCode == -1) return;
            WriteValueMenu();
            double value = GetSignalValue();
            try
            {
                InputProxy.SendSignal(signalCode, value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void InteractForHistoryReading()
        {
            WriteSignalMenuReading();
            int signalCode = GetSignalCode();
            if (signalCode == -1) return;
            WriteBeginDateMenu();
            string beginDate = GetDateValue();
            WriteEndDateMenu();
            string endDate = GetDateValue();
            try
            {
                string allValues = ReadingProxy.ReadFromHistory(beginDate, endDate, signalCode);
                Console.WriteLine("Following values exist within given timeframe:\n" + allValues);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private string GetDateValue()
        {
            while (true)
            {
                try
                {
                    string date = Console.ReadLine();
                    return date;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occured reading input.");
                }
            }
        }


        private int GetSignalCode()
        {
            while (true)
            {
                try
                {
                    int signalCode = int.Parse(Console.ReadLine());
                    return signalCode;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occured reading input.");
                }
            }
        }


        private int GetCommand()
        {
            while (true)
            {
                try
                {
                    int command = int.Parse(Console.ReadLine());
                    return command;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occured reading input.");
                }
            }
        }


        private double GetSignalValue()
        {
            while (true)
            {
                try
                {
                    double signalValue = double.Parse(Console.ReadLine());
                    return signalValue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occured reading input.");
                }
            }
        }


        private void WriteBeginDateMenu()
        {
            Console.WriteLine("---READING FROM HISTORY---");
            Console.WriteLine("What is the starting date for the timeframe you wish to read?");
            Console.WriteLine("Value should be a date in (dd-MM-yyy hh:mm:ss) format.");
        }

        private void WriteEndDateMenu()
        {
            Console.WriteLine("---READING FROM HISTORY---");
            Console.WriteLine("What is the ending date for the timeframe you wish to read?");
            Console.WriteLine("Value should be a date in (dd-MM-yyy hh:mm:ss) format.");
        }

        private void WriteMainMenu()
        {
            Console.WriteLine("---MENU---");
            Console.WriteLine("1 - Read data with timeframe");
            Console.WriteLine("2 - Write directly to history");
            Console.WriteLine("0 - Exit");
        }


        private void WriteSignalMenuReading()
        {
            Console.WriteLine("---READING FROM HISTORY---");
            Console.WriteLine("What signal do you wish to read?");
            Console.WriteLine("0 - Analog");
            Console.WriteLine("1 - Digital");
            Console.WriteLine("2 - Custom");
            Console.WriteLine("3 - Limitset");
            Console.WriteLine("4 - Singlenode");
            Console.WriteLine("5 - Multiplenode");
            Console.WriteLine("6 - Consumer");
            Console.WriteLine("7 - Source");
            Console.WriteLine("-1 - Exit");
        }


        private void WriteSignalMenu()
        {
            Console.WriteLine("---WRITING TO HISTORY---");
            Console.WriteLine("What signal do you wish to input?");
            Console.WriteLine("0 - Analog");
            Console.WriteLine("1 - Digital");
            Console.WriteLine("2 - Custom");
            Console.WriteLine("3 - Limitset");
            Console.WriteLine("4 - Singlenode");
            Console.WriteLine("5 - Multiplenode");
            Console.WriteLine("6 - Consumer");
            Console.WriteLine("7 - Source");
            Console.WriteLine("-1 - Exit");
        }


        private void WriteValueMenu()
        {
            Console.WriteLine("---WRITING TO HISTORY---");
            Console.WriteLine("What value do you wish to input?");
            Console.WriteLine("Value should be a real number.");
        }

        public void ActivateUserInteraction()
        {
            while (true)
            {
                WriteMainMenu();
                int command = GetCommand();

                switch (command)
                {
                    case 1:
                        InteractForHistoryReading();
                        break;

                    case 2:
                        InteractForHistoryWriting();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Command not recognized");
                        break;
                }

            }
        }
    }
}
