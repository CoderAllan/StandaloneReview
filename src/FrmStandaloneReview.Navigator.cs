namespace StandaloneReview
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using Microsoft.VisualBasic.PowerPacks;


    public partial class FrmStandaloneReview
    {
        private readonly Dictionary<int, RectangleShape> _navigatorCommentRectangles = new Dictionary<int, RectangleShape>();
        private RectangleShape _navigatorCurrentLineRectangle;

        private RectangleShape CreateRectangleShape(int line, Color color)
        {
            double correctionFactor = 1;
            var editor = GetActiveTextEditor();
            if (editor != null)
            {
                if (navigatorCanvas.Height - navigatorCanvas.Top < editor.Document.TotalNumberOfLines)
                {
                    correctionFactor = (navigatorCanvas.Height - navigatorCanvas.Top) / (double)editor.Document.TotalNumberOfLines;
                }
                var rectangleShape = new RectangleShape
                {
                    Height = 2,
                    Top = navigatorCanvas.Top + (int)(line * correctionFactor),
                    Width = navigatorCanvas.Width - 2,
                    Left = Width - 42, // This calculation should be: 'Left = navigatorCanvas.Left + 1', but the navigatorCanvas.Left property never changes when resizing the form. So we have to use the magic number 42 to make the placement of the navigator lines work.
                    FillStyle = FillStyle.Solid,
                    FillColor = color,
                    BorderStyle = DashStyle.Solid,
                    BorderColor = color,
                    Tag = line,
                    Cursor = Cursors.Hand,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                };
                rectangleShape.Click += rectangle_Click;
                navigatorCanvas.SendToBack();
                rectangleShape.BringToFront();
                return rectangleShape;
            }
            return null;
        }

        public void rectangle_Click(object sender, EventArgs e)
        {
            var rectangle = (RectangleShape)sender;
            var line = (int)rectangle.Tag;
            var editor = GetActiveTextEditor();
            editor.ActiveTextAreaControl.Caret.Line = line - 1;
        }

        public void AddNavigatorCurrentLineMarker(int line)
        {
            if (_navigatorCurrentLineRectangle != null)
            {
                shapeContainer1.Shapes.Remove(_navigatorCurrentLineRectangle);
            }
            _navigatorCurrentLineRectangle = CreateRectangleShape(line, Color.DarkBlue);
            if (_navigatorCurrentLineRectangle != null)
            {
                shapeContainer1.Shapes.Add(_navigatorCurrentLineRectangle);
            }
        }

        public void AddNavigatorCommentMarker(int line)
        {
            if (!_navigatorCommentRectangles.ContainsKey(line))
            {
                var rectangle = CreateRectangleShape(line, Color.Goldenrod);
                if (rectangle != null)
                {
                    _navigatorCommentRectangles.Add(line, rectangle);
                    shapeContainer1.Shapes.Add(rectangle);
                }
            }
        }

        public void AddGreyedArea()
        {
            var editor = GetActiveTextEditor();
            if (editor != null)
            {
                var rectangleShape = new RectangleShape
                {
                    Height = navigatorCanvas.Height - editor.Document.TotalNumberOfLines,
                    Top = navigatorCanvas.Top + editor.Document.TotalNumberOfLines + 2,
                    Width = navigatorCanvas.Width - 2,
                    Left = navigatorCanvas.Left + 1,
                    FillStyle = FillStyle.Solid,
                    FillColor = Color.LightGray,
                    BorderStyle = DashStyle.Solid,
                    BorderColor = Color.LightGray,
                    Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom,
                };
                shapeContainer1.Shapes.Add(rectangleShape);
            }
        }

        public void RemoveAllNavigatorShapes()
        {
            shapeContainer1.Shapes.Clear();
            _navigatorCommentRectangles.Clear();
        }

        public void RemoveNavigatorCommentMarker(int line)
        {
            if (_navigatorCommentRectangles.ContainsKey(line))
            {
                shapeContainer1.Shapes.Remove(_navigatorCommentRectangles[line]);
                _navigatorCommentRectangles.Remove(line);
            }
        }
    }
}
