using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Conway
{
    public class Drawer : IDisposable
    {
        public static int Offset = 20;

        private readonly SolidBrush _brush;
        private readonly Graphics _graphics;
        private readonly PictureBox _pb;

        public Drawer(PictureBox pb, SolidBrush brush)
        {
            Contract.Assert(_pb == null, "No picture box, no drawings");
            _pb = pb;
           
            if(pb.Image == null)
                pb.Image = new Bitmap(pb.Width, pb.Height);

            _graphics = Graphics.FromImage(pb.Image);
            _brush = brush ?? new SolidBrush(Color.Black);
        }

        public IEnumerable<Rectangle> Project(Universe universe)
        {
            return universe.AliveCells.Select(c => new Rectangle(c.X * (Offset + 1), c.Y * (Offset + 1), Offset, Offset));
        }

        public void Draw(Universe universe)
        {
            _graphics.Clear(_pb.BackColor);

            foreach (var cell in Project(universe))
            {
                _graphics.FillRectangle(_brush, cell);
            }

            _pb.Invalidate();
        }

        public void Dispose()
        {
            _brush?.Dispose();
            _graphics?.Dispose();
        }
    }
}
