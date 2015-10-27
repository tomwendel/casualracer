using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace CasualRacer.Model
{
    internal class Player : INotifyPropertyChanged
    {
        private bool wheelLeft = false;
        private bool wheelRight = false;
        private float direction = 0f;
        private Vector position = new Vector();

        public float Direction
        {
            get { return direction; }
            set
            {
                if (direction != value)
                {
                    direction = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Direction"));
                }
            }
        }

        public Vector Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Position"));
                }
            }
        }

        public bool Acceleration { get; set; }

        private bool InternalWheelLeft { get; set; }

        private bool InternalWheelRight { get; set; }

        public bool WheelLeft
        {
            get { return wheelLeft; }
            set
            {
                if (wheelLeft != value)
                {
                    wheelLeft = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("WheelLeft"));
                }
            }
        }

        public bool WheelRight
        {
            get { return wheelRight; }
            set
            {
                if (wheelRight != value)
                {
                    wheelRight = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("WheelRight"));
                }
            }
        }

        public Player()
        {
            this.PropertyChanged += Player_PropertyChanged;
        }

        private void Player_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WheelLeft")
            {
                InternalWheelLeft = WheelLeft;
                InternalWheelRight = !WheelLeft && WheelRight;
            }
            if (e.PropertyName == "WheelRight")
            {
                InternalWheelLeft = !WheelRight && WheelLeft;
                InternalWheelRight = WheelRight;
            }
        }

        public void Update(TimeSpan totalTime, TimeSpan elapsedTime)
        {
            if (InternalWheelLeft)
                Direction -= (float)elapsedTime.TotalSeconds * 100;
            if (InternalWheelRight)
                Direction += (float)elapsedTime.TotalSeconds * 100;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
