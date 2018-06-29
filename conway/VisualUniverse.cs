using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace Conway
{
    public partial class VisualUniverse : Form
    {
        private readonly Universe _universe;
        private readonly Drawer _drawer;
        private bool _toggled;

        private readonly Bitmap _black = new Bitmap(20,20);
        private readonly Bitmap _control = new Bitmap(20, 20);

        public VisualUniverse()
        {
            InitializeComponent();

            dgButtons.RowCount = 15;
            for (var i = 0; i < dgButtons.RowCount; i++)
            for (var j = 0; j < dgButtons.ColumnCount; j++)
            {
                dgButtons[i, j].Value = _control;
            }

            pbUniverse.Visible = false;
            pbUniverse.Image = new Bitmap(pbUniverse.Width, pbUniverse.Height);

            using (var gfx = Graphics.FromImage(_black))
            using (var brush = new SolidBrush(Color.Black))
            {
                gfx.FillRectangle(brush, 0, 0, 20, 20);
            }

            using (var gfx = Graphics.FromImage(_control))
            using (var brush = new SolidBrush(SystemColors.Control))
            {
                gfx.FillRectangle(brush, 0, 0, 20, 20);
            }

            _drawer = new Drawer(pbUniverse, new SolidBrush(Color.Black));
            _universe = new Universe();

            Observable.FromEventPattern(h => btnToggle.Click += h, h => btnToggle.Click -= h).Subscribe(_ => _toggled = !_toggled);
            Observable.Interval(TimeSpan.FromMilliseconds(500)).Subscribe(_ => { if (_toggled) Visualize(); });
        }

        private void Visualize()
        {
            _drawer.Draw(_universe);
            _universe.Evolve();
        }

        private void btnSeed_Click(object sender, EventArgs e)
        {
            var aliveCells = new List<Point>();

            for (var i = 0; i < dgButtons.RowCount; i++)
            for (var j = 0; j < dgButtons.ColumnCount; j++)
            {
                if (dgButtons[i, j].Value == _black)
                    aliveCells.Add(new Point(i, j));
            }

            if (aliveCells.Any())
            {
                btnSeed.Visible = false;
                dgButtons.Visible = false;
                pbUniverse.Visible = true;
                btnToggle.Visible = true;

                _universe.AliveCells.Clear();
                _universe.AliveCells.AddRange(aliveCells);

                Visualize();
            }
        }

        private void dgButtons_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null) return;

            var cell = grid[e.ColumnIndex, e.RowIndex];
            cell.Value = cell.Value == _black ? _control : _black;
        }
    }
}
