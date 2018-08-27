using RobotEditor.Windows.Base;
using RobotRuntime;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CustomNamespace2
{
    [Serializable]
    public class NewPainter : MarshalByRefObject, IPaintOnScreen
    {
        public NewPainter() { }

        public event Action Invalidate;
        public event Action<IPaintOnScreen> StartInvalidateOnTimer;
        public event Action<IPaintOnScreen> StopInvalidateOnTimer;

        public void OnPaint(PaintEventArgs e)
        {
            //var g = e.Graphics;
            //g.DrawString("something", Fonts.Big, Brushes.Red, new PointF(100, 300));
        }
    }
}
