using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;

namespace BMonGWUI
{
    public class DoubleTrackBar : Control
    {

        public DoubleTrackBar()
        {
            this.DoubleBuffered = true;
            this.SetDefaults();
        }

        private void SetDefaults()
        {
            this.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.SmallChange = 1;
            this.Maximum = 10;
            this.Minimum = 0;
            this.ValueLeft = 0;
            this.ValueRight = 7;
        }

        #region " Private Fields "

        private TrackBarThumbState leftThumbState;

        private TrackBarThumbState rightThumbState;
        private bool draggingLeft;
        #endregion
        private bool draggingRight;

        #region " Enums "

        public enum Thumbs
        {
            None = 0,
            Left = 1,
            Right = 2
        }

        #endregion

        #region " Properties "

        private Thumbs _SelectedThumb;
        /// <summary>
        /// Gets the thumb that had focus last.
        /// </summary>
        /// <returns>The thumb that had focus last.</returns>
        [Description("The thumb that had focus last.")]
        public Thumbs SelectedThumb
        {
            get { return _SelectedThumb; }
            private set { _SelectedThumb = value; }
        }

        private int _ValueLeft;
        /// <summary>
        /// Gets or sets the position of the left slider.
        /// </summary>
        /// <returns>The position of the left slider.</returns>
        [Description("The position of the left slider.")]
        public int ValueLeft
        {
            get { return _ValueLeft; }
            set
            {
                if (value < this.Minimum || value > this.Maximum)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'ValueLeft'. 'ValueLeft' should be between 'Minimum' and 'Maximum'.", value.ToString()), "ValueLeft");
                }
                if (value > this.ValueRight)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'ValueLeft'. 'ValueLeft' should be less than or equal to 'ValueRight'.", value.ToString()), "ValueLeft");
                }
                _ValueLeft = value;

                this.OnValueChanged(EventArgs.Empty);
                this.OnLeftValueChanged(EventArgs.Empty);

                this.Invalidate();
            }
        }

        private int _ValueRight;
        /// <summary>
        /// Gets or sets the position of the right slider.
        /// </summary>
        /// <returns>The position of the right slider.</returns>
        [Description("The position of the right slider.")]
        public int ValueRight
        {
            get { return _ValueRight; }
            set
            {
                if (value < this.Minimum || value > this.Maximum)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'ValueRight'. 'ValueRight' should be between 'Minimum' and 'Maximum'.", value.ToString()), "ValueRight");
                }
                if (value < this.ValueLeft)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'ValueRight'. 'ValueRight' should be greater than or equal to 'ValueLeft'.", value.ToString()), "ValueLeft");
                }
                _ValueRight = value;

                this.OnValueChanged(EventArgs.Empty);
                this.OnRightValueChanged(EventArgs.Empty);

                this.Invalidate();
            }
        }

        private int _Minimum;
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <returns>The minimum value.</returns>
        [Description("The minimum value.")]
        public int Minimum
        {
            get { return _Minimum; }
            set
            {
                if (value >= this.Maximum)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'Minimum'. 'Minimum' should be less than 'Maximum'.", value.ToString()), "Minimum");
                }
                _Minimum = value;
                this.Invalidate();
            }
        }

        private int _Maximum;
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <returns>The maximum value.</returns>
        [Description("The maximum value.")]
        public int Maximum
        {
            get { return _Maximum; }
            set
            {
                if (value <= this.Minimum)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'Maximum'. 'Maximum' should be greater than 'Minimum'.", value.ToString()), "Maximum");
                }
                _Maximum = value;
                this.Invalidate();
            }
        }

        private Orientation _Orientation;
        /// <summary>
        /// Gets or sets the orientation of the control.
        /// </summary>
        /// <returns>The orientation of the control.</returns>
        [Description("The orientation of the control.")]
        public Orientation Orientation
        {
            get { return _Orientation; }
            set { _Orientation = value; }
        }

        private int _SmallChange;
        /// <summary>
        /// Gets or sets the amount of positions the closest slider moves when the control is clicked.
        /// </summary>
        /// <returns>The amount of positions the closest slider moves when the control is clicked.</returns>
        [Description("The amount of positions the closest slider moves when the control is clicked.")]
        public int SmallChange
        {
            get { return _SmallChange; }
            set { _SmallChange = value; }
        }

        private double RelativeValueLeft
        {
            get
            {
                dynamic diff = this.Maximum - this.Minimum;
                return diff == 0 ? this.ValueLeft : this.ValueLeft / diff;
            }
        }

        private double RelativeValueRight
        {
            get
            {
                dynamic diff = this.Maximum - this.Minimum;
                return diff == 0 ? this.ValueLeft : this.ValueRight / diff;
            }
        }

        #endregion

        #region " Methods "

        public void IncrementLeft()
        {
            dynamic newValue = Math.Min(this.ValueLeft + 1, this.Maximum);
            if (this.IsValidValueLeft(newValue))
            {
                this.ValueLeft = newValue;
            }
            this.Invalidate();
        }

        public void IncrementRight()
        {
            dynamic newValue = Math.Min(this.ValueRight + 1, this.Maximum);
            if (this.IsValidValueRight(newValue))
            {
                this.ValueRight = newValue;
            }
            this.Invalidate();
        }

        public void DecrementLeft()
        {
            dynamic newValue = Math.Max(this.ValueLeft - 1, this.Minimum);
            if (this.IsValidValueLeft(newValue))
            {
                this.ValueLeft = newValue;
            }
            this.Invalidate();
        }

        public void DecrementRight()
        {
            dynamic newValue = Math.Max(this.ValueRight - 1, this.Minimum);
            if (this.IsValidValueRight(newValue))
            {
                this.ValueRight = newValue;
            }
            this.Invalidate();
        }

        private bool IsValidValueLeft(int value)
        {
            return (value >= this.Minimum && value <= this.Maximum && value < this.ValueRight);
        }

        private bool IsValidValueRight(int value)
        {
            return (value >= this.Minimum && value <= this.Maximum && value > this.ValueLeft);
        }

        private Rectangle GetLeftThumbRectangle(Graphics g = null)
        {
            dynamic shouldDispose = (g == null);
            if (shouldDispose)
                g = this.CreateGraphics();

            dynamic rect = this.GetThumbRectangle(this.RelativeValueLeft, g);
            if (shouldDispose)
                g.Dispose();

            return rect;
        }

        private Rectangle GetRightThumbRectangle(Graphics g = null)
        {
            dynamic shouldDispose = (g == null);
            if (shouldDispose)
                g = this.CreateGraphics();

            dynamic rect = this.GetThumbRectangle(this.RelativeValueRight, g);
            if (shouldDispose)
                g.Dispose();

            return rect;
        }

        private Rectangle GetThumbRectangle(double relativeValue, Graphics g)
        {
            dynamic size = TrackBarRenderer.GetBottomPointingThumbSize(g, TrackBarThumbState.Normal);
            dynamic border = Convert.ToInt32(size.Width / 2);
            dynamic w = this.GetTrackRectangle(border).Width;
            dynamic x = Convert.ToInt32(Math.Abs(this.Minimum) / (this.Maximum - this.Minimum) * w + relativeValue * w);

            dynamic y = Convert.ToInt32((this.Height - size.Height) / 2);
            return new Rectangle(new Point(x, y), size);
        }

        private Rectangle GetTrackRectangle(int border)
        {
            //TODO: Select Case for hor/ver
            return new Rectangle(border, Convert.ToInt32(this.Height / 2) - 3, this.Width - 2 * border - 1, 4);
        }

        private Thumbs GetClosestSlider(Point point)
        {
            dynamic leftThumbRect = this.GetLeftThumbRectangle();
            dynamic rightThumbRect = this.GetRightThumbRectangle();
            if (this.Orientation == System.Windows.Forms.Orientation.Horizontal)
            {
                if (Math.Abs(leftThumbRect.X - point.X) > Math.Abs(rightThumbRect.X - point.X) && Math.Abs(leftThumbRect.Right - point.X) > Math.Abs(rightThumbRect.Right - point.X))
                {
                    return Thumbs.Right;
                }
                else
                {
                    return Thumbs.Left;
                }
            }
            else
            {
                if (Math.Abs(leftThumbRect.Y - point.Y) > Math.Abs(rightThumbRect.Y - point.Y) && Math.Abs(leftThumbRect.Bottom - point.Y) > Math.Abs(rightThumbRect.Bottom - point.Y))
                {
                    return Thumbs.Right;
                }
                else
                {
                    return Thumbs.Left;
                }
            }
        }

        private void SetThumbState(Point location, TrackBarThumbState newState)
        {
            dynamic leftThumbRect = this.GetLeftThumbRectangle();
            dynamic rightThumbRect = this.GetRightThumbRectangle();

            if (leftThumbRect.Contains(location))
            {
                leftThumbState = newState;
            }
            else
            {
                if (this.SelectedThumb == Thumbs.Left)
                {
                    leftThumbState = TrackBarThumbState.Hot;
                }
                else
                {
                    leftThumbState = TrackBarThumbState.Normal;
                }
            }

            if (rightThumbRect.Contains(location))
            {
                rightThumbState = newState;
            }
            else
            {
                if (this.SelectedThumb == Thumbs.Right)
                {
                    rightThumbState = TrackBarThumbState.Hot;
                }
                else
                {
                    rightThumbState = TrackBarThumbState.Normal;
                }
            }
        }

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.SetThumbState(e.Location, TrackBarThumbState.Hot);

            dynamic offset = Convert.ToInt32(e.Location.X / (this.Width) * (this.Maximum - this.Minimum));
            dynamic newValue = this.Minimum + offset;
            if (draggingLeft)
            {
                if (this.IsValidValueLeft(newValue))
                    this.ValueLeft = newValue;
            }
            else if (draggingRight)
            {
                if (this.IsValidValueRight(newValue))
                    this.ValueRight = newValue;
            }

            this.Invalidate();
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Focus();
            this.SetThumbState(e.Location, TrackBarThumbState.Pressed);

            draggingLeft = (leftThumbState == TrackBarThumbState.Pressed);
            if (!draggingLeft)
                draggingRight = (rightThumbState == TrackBarThumbState.Pressed);

            if (draggingLeft)
            {
                this.SelectedThumb = Thumbs.Left;
            }
            else if (draggingRight)
            {
                this.SelectedThumb = Thumbs.Right;
            }

            if (!draggingLeft && !draggingRight)
            {
                if ((int)this.GetClosestSlider(e.Location) == 1)
                {
                    if (e.X < this.GetLeftThumbRectangle().X)
                    {
                        this.DecrementLeft();
                    }
                    else
                    {
                        this.IncrementLeft();
                    }
                    this.SelectedThumb = Thumbs.Left;
                }
                else
                {
                    if (e.X < this.GetRightThumbRectangle().X)
                    {
                        this.DecrementRight();
                    }
                    else
                    {
                        this.IncrementRight();
                    }
                    this.SelectedThumb = Thumbs.Right;
                }
            }

            this.Invalidate();
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggingLeft = false;
            draggingRight = false;
            this.Invalidate();
        }

        protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta == 0)
                return;

            if ((int)this.SelectedThumb == 1)
            {
                if (e.Delta > 0)
                {
                    this.IncrementLeft();
                }
                else
                {
                    this.DecrementLeft();
                }
            }
            else if ((int)this.SelectedThumb == 2)
            {
                if (e.Delta > 0)
                {
                    this.IncrementRight();
                }
                else
                {
                    this.DecrementRight();
                }
            }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            dynamic thumbSize = this.GetThumbRectangle(0, e.Graphics).Size;
            dynamic trackRect = this.GetTrackRectangle(Convert.ToInt32(thumbSize.Width / 2));
            dynamic ticksRect = trackRect;
            ticksRect.Offset(0, 15);

            TrackBarRenderer.DrawVerticalTrack(e.Graphics, trackRect);
            TrackBarRenderer.DrawHorizontalTicks(e.Graphics, ticksRect, this.Maximum - this.Minimum + 1, EdgeStyle.Etched);
            TrackBarRenderer.DrawBottomPointingThumb(e.Graphics, this.GetLeftThumbRectangle(e.Graphics), leftThumbState);
            TrackBarRenderer.DrawBottomPointingThumb(e.Graphics, this.GetRightThumbRectangle(e.Graphics), rightThumbState);
        }

        #endregion

        #region " Events "

        public event EventHandler ValueChanged;
        public event EventHandler LeftValueChanged;
        public event EventHandler RightValueChanged;

        protected virtual void OnValueChanged(EventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        protected virtual void OnLeftValueChanged(EventArgs e)
        {
            if (LeftValueChanged != null)
            {
                LeftValueChanged(this, e);
            }
        }

        protected virtual void OnRightValueChanged(EventArgs e)
        {
            if (RightValueChanged != null)
            {
                RightValueChanged(this, e);
            }
        }

        #endregion

    }
}