using System;
using System.Diagnostics;
using System.Threading;
using Windows.Devices.Gpio;

namespace SenserTest
{
    public class Senser
    {
        private GpioController gpio;

        private GpioPin triggerPin;
        private GpioPin echoPin;

        private int PIN_ECHO = 18; // Gpio 18
        private int PIN_TRIG = 23; // Gpio 23

        public static Senser Distance { get; } = new Senser();

        private Senser()
        {
            gpio = GpioController.GetDefault();
            if (gpio == null)
            {
                return;
            }

            this.triggerPin = gpio.OpenPin(PIN_TRIG);
            this.echoPin = gpio.OpenPin(PIN_ECHO);

            this.triggerPin.SetDriveMode(GpioPinDriveMode.Output);
            this.echoPin.SetDriveMode(GpioPinDriveMode.Input);

            this.triggerPin.Write(GpioPinValue.Low);
        }

        public double GetDistance()
        {
            ManualResetEvent mre = new ManualResetEvent(false);
            Stopwatch stopwatch = new Stopwatch();

            this.triggerPin.Write(GpioPinValue.High);
            mre.WaitOne(TimeSpan.FromMilliseconds(0.01));
            this.triggerPin.Write(GpioPinValue.Low);

            while (this.echoPin.Read() == GpioPinValue.Low)
            {
            }
            stopwatch.Start();

            while (this.echoPin.Read() == GpioPinValue.High)
            {
            }
            stopwatch.Stop();

            TimeSpan timeBetween = stopwatch.Elapsed;
            Debug.WriteLine(timeBetween.ToString());
            double distance = timeBetween.TotalSeconds * 17000;

            return Math.Round(distance);
        }
    }
}