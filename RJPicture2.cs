using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;

namespace Chat_video_app
{
    internal class RJPicture2 : PictureBox
    {
        //Fields
        private int borderSize = 2;
        private Color borderColor = Color.RoyalBlue;
        private Color borderColor2 = Color.HotPink;
        private DashStyle borderLineStyle = DashStyle.Solid;
        private DashCap borderCapStyle = DashCap.Flat;
        private float gradientAngle = 50F;
        private int borderRadius = 10; // Độ cong của góc
        private string displayText = ""; // Chữ để hiển thị
        private Font displayFont = new Font("Arial", 12); // Kiểu chữ để hiển thị
        private Color displayColor = Color.Black; // Màu chữ để hiển thị
        private Point textLocation = new Point(0, 0); // Vị trí của chữ

        //Constructor
        public RJPicture2()
        {
            this.Size = new Size(100, 100);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        //Properties
        [Category("RJ Code Advance")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color BorderColor2
        {
            get { return borderColor2; }
            set
            {
                borderColor2 = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public DashStyle BorderLineStyle
        {
            get { return borderLineStyle; }
            set
            {
                borderLineStyle = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public DashCap BorderCapStyle
        {
            get { return borderCapStyle; }
            set
            {
                borderCapStyle = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public float GradientAngle
        {
            get { return gradientAngle; }
            set
            {
                gradientAngle = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public string DisplayText
        {
            get { return displayText; }
            set
            {
                displayText = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Font DisplayFont
        {
            get { return displayFont; }
            set
            {
                displayFont = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Color DisplayColor
        {
            get { return displayColor; }
            set
            {
                displayColor = value;
                this.Invalidate();
            }
        }
        [Category("RJ Code Advance")]
        public Point TextLocation
        {
            get { return textLocation; }
            set
            {
                textLocation = value;
                this.Invalidate();
            }
        }

        //Overridden methods
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            //Fields
            var graph = pe.Graphics;
            var rectContourSmooth = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var rectBorder = Rectangle.Inflate(rectContourSmooth, -borderSize, -borderSize);
            var smoothSize = borderSize > 0 ? borderSize * 3 : 1;
            using (var borderGColor = new LinearGradientBrush(rectBorder, borderColor, borderColor2, gradientAngle))
            using (var pathRegion = CreateRoundedRectangle(rectContourSmooth, borderRadius))
            using (var penSmooth = new Pen(this.Parent.BackColor, smoothSize))
            using (var penBorder = new Pen(borderGColor, borderSize))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                penBorder.DashStyle = borderLineStyle;
                penBorder.DashCap = borderCapStyle;
                //Set rounded region 
                this.Region = new Region(pathRegion);
                //Drawing
                graph.DrawPath(penSmooth, pathRegion); // Sử dụng hàm DrawPath thay vì DrawEllipse để vẽ hình vuông với góc bo tròn
                if (borderSize > 0) //Draw border
                    graph.DrawPath(penBorder, pathRegion);

                // Draw display text
                if (!string.IsNullOrEmpty(displayText))
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    using (SolidBrush brush = new SolidBrush(displayColor))
                    {
                        graph.DrawString(displayText, displayFont, brush, new PointF(textLocation.X, textLocation.Y), sf);
                    }
                }
            }
        }

        // Custom method to create rounded rectangle path
        private GraphicsPath CreateRoundedRectangle(Rectangle rectangle, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(rectangle.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(rectangle);
                return path;
            }

            // Top left arc
            path.AddArc(arc, 180, 90);
            // Top right arc
            arc.X = rectangle.Right - diameter;
            path.AddArc(arc, 270, 90);
            // Bottom right arc
            arc.Y = rectangle.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            // Bottom left arc
            arc.X = rectangle.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
