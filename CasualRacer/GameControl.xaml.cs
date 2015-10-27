using CasualRacer.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CasualRacer
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        private Game game = new Game();

        private DispatcherTimer timer = new DispatcherTimer();
        private Stopwatch totalWatch = new Stopwatch();
        private Stopwatch elapsedWatch = new Stopwatch();
        private Thread keyboardThread;
        private volatile bool shouldStopKeyboardThread;

        public GameControl()
        {
            InitializeComponent();
            DataContext = game;

            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += Timer_Tick;
            timer.IsEnabled = true;

            totalWatch.Start();
            elapsedWatch.Start();

            keyboardThread = new Thread(new ThreadStart(KeyboardThread));
            keyboardThread.SetApartmentState(ApartmentState.STA);

            Application.Current.Exit += (sender, args) => this.Stop();
        }

        public void Start()
        {
            shouldStopKeyboardThread = false;
            keyboardThread.Start();
        }

        public void Stop()
        {
            shouldStopKeyboardThread = true;
            keyboardThread.Join();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = elapsedWatch.Elapsed;
            elapsedWatch.Restart();
            game.Update(totalWatch.Elapsed, elapsed);
        }

        private void KeyboardThread()
        {
            while (!shouldStopKeyboardThread)
            {
                this.game.Player1.Acceleration = Keyboard.IsKeyDown(Key.Up);
                this.game.Player1.WheelLeft = Keyboard.IsKeyDown(Key.Left);
                this.game.Player1.WheelRight = Keyboard.IsKeyDown(Key.Right);

                Thread.Sleep(50);
            }
        }
    }
}
