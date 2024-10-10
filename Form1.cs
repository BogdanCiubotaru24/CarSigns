using System.Text;

namespace PrimulMeuCursDeGrafica
{
    public partial class Form1 : Form
    {
        MyGraphics myGraphics;
        static Random rnd = new Random();
        float a = 0, b = 0;

        public Form1()
        {
            InitializeComponent();
            myGraphics = new MyGraphics(pictureBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Start";

            DrawStopSign(myGraphics.grp, new PointF(150, 250), 100);
            DrawYieldSign(myGraphics.grp, new PointF(400, 250), 100);
            DrawVaticanFlag(myGraphics.grp, new PointF(650, 150), 200, 150);
            DrawNoEntrySign(myGraphics.grp, new PointF(150, 500), 100);
            DrawEmptyCircleSign(myGraphics.grp, new PointF(400, 500), 100);
            DrawPriorityRoadSign(myGraphics.grp, new PointF(650, 500), 100, (float)Math.PI / 2);
            myGraphics.Refresh();
        }

        private static List<PointF> RegularPolygon(int n, PointF C, float R, float fi)
        {
            List<PointF> toReturn = new List<PointF>();

            float alpha = (float)(2 * Math.PI) / n;

            for (int i = 0; i < n; i++)
            {
                float x = C.X + R * (float)Math.Cos(i * alpha + fi);
                float y = C.Y + R * (float)Math.Sin(i * alpha + fi);
                toReturn.Add(new PointF(x, y));
            }
            return toReturn;
        }

        public void DrawPolygon(Graphics grp, List<PointF> points)
        {
            grp.DrawPolygon(new Pen(Color.Black, 3), points.ToArray());
        }

        public void DrawStopSign(Graphics grp, PointF center, float radius)
        {
            // Draw an octagon for the stop sign
            List<PointF> points = RegularPolygon(8, center, radius, (float)Math.PI / 8);
            grp.FillPolygon(Brushes.Red, points.ToArray());
            grp.DrawPolygon(new Pen(Color.White, 3), points.ToArray());

            // Draw the text "STOP" in the center
            using (Font font = new Font("Arial", radius / 3, FontStyle.Bold, GraphicsUnit.Pixel))
            {
                SizeF textSize = grp.MeasureString("STOP", font);
                PointF textPosition = new PointF(center.X - textSize.Width / 2, center.Y - textSize.Height / 2);
                grp.DrawString("STOP", font, Brushes.White, textPosition);
            }
        }

        public void DrawYieldSign(Graphics grp, PointF center, float radius)
        {
            // Draw a triangle for the yield sign
            List<PointF> points = RegularPolygon(3, center, radius, (float)Math.PI / 2);
            grp.FillPolygon(Brushes.Red, points.ToArray());
            grp.DrawPolygon(new Pen(Color.Black, 3), points.ToArray());

            // Draw an inverted white triangle inside
            List<PointF> innerPoints = RegularPolygon(3, center, radius * 0.7f, (float)Math.PI / 2);
            grp.FillPolygon(Brushes.White, innerPoints.ToArray());
        }

        public void DrawVaticanFlag(Graphics grp, PointF topLeft, float width, float height)
        {
            // Draw the yellow part of the flag
            RectangleF yellowRect = new RectangleF(topLeft.X, topLeft.Y, width / 2, height);
            grp.FillRectangle(Brushes.Yellow, yellowRect);

            // Draw the white part of the flag
            RectangleF whiteRect = new RectangleF(topLeft.X + width / 2, topLeft.Y, width / 2, height);
            grp.FillRectangle(Brushes.White, whiteRect);
        }

        public void DrawNoEntrySign(Graphics grp, PointF center, float radius)
        {
            // Draw a red circle
            grp.FillEllipse(Brushes.Red, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            grp.DrawEllipse(new Pen(Color.Black, 3), center.X - radius, center.Y - radius, radius * 2, radius * 2);

            // Draw a horizontal white rectangle in the center
            float rectWidth = radius * 1.5f;
            float rectHeight = radius * 0.3f;
            RectangleF rect = new RectangleF(center.X - rectWidth / 2, center.Y - rectHeight / 2, rectWidth, rectHeight);
            grp.FillRectangle(Brushes.White, rect);
        }

        public void DrawEmptyCircleSign(Graphics grp, PointF center, float radius)
        {
            // Draw a red circle with white inside
            grp.FillEllipse(Brushes.White, center.X - radius, center.Y - radius, radius * 2, radius * 2);
            grp.DrawEllipse(new Pen(Color.Red, 10), center.X - radius, center.Y - radius, radius * 2, radius * 2);
        }

        public void DrawPriorityRoadSign(Graphics grp, PointF center, float radius, float rotation = 0)
        {
            // Draw a diamond-shaped sign
            List<PointF> points = RegularPolygon(4, center, radius, rotation);
            grp.FillPolygon(Brushes.White, points.ToArray());
            grp.DrawPolygon(new Pen(Color.Black, 3), points.ToArray());

            // Draw a smaller yellow diamond inside
            List<PointF> innerPoints = RegularPolygon(4, center, radius * 0.6f, rotation);
            grp.FillPolygon(Brushes.Yellow, innerPoints.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            a += 10;
            b += 0.1f;

            if (a >= 200)
                a = 10;

            myGraphics.Clear();
            DrawStopSign(myGraphics.grp, new PointF(150, 250), 100);
            DrawYieldSign(myGraphics.grp, new PointF(400, 250), 100);
            DrawVaticanFlag(myGraphics.grp, new PointF(650, 150), 200, 150);
            DrawNoEntrySign(myGraphics.grp, new PointF(150, 500), 100);
            DrawEmptyCircleSign(myGraphics.grp, new PointF(400, 500), 100);
            DrawPriorityRoadSign(myGraphics.grp, new PointF(650, 500), 100, (float)Math.PI / 4);
            myGraphics.Refresh();
        }
    }
}