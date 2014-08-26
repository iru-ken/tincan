using BitMiracle.Docotic.Pdf;

namespace JavascriptInPdf
{
    class Date
    {
        private const double TopMargin = 50;   // the height of the top margin of the document
        private const double RightMargin = 50; // the width of the right margin of the document

        private const double FontSize = 10; // font size in the document

        private static readonly string[] Months =
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        private PdfDocument m_document;
        private PdfPage m_page;
        private PdfCanvas m_canvas;

        private PdfJavaScriptAction m_validateNumeric;

        public void Run()
        {
            m_document = new PdfDocument();
            m_page = m_document.Pages[0];
            m_canvas = m_page.Canvas;

            setupFont(m_document.AddFont("Arial"));
            addDateField();

            m_document.OnOpenDocument = m_document.CreateJavaScriptAction(Resources.SetCurrentDate);

            m_document.Save("Date.pdf");
            System.Diagnostics.Process.Start("Date.pdf");
        }

        private void setupFont(PdfFont font)
        {
            m_canvas.Font = font;
            m_canvas.FontSize = FontSize;
        }

        private void addDateField()
        {
            m_canvas.FontSize = FontSize;

            double left = m_page.Width - RightMargin - 150;
            double top = TopMargin;

            double height = addDayField(left, top);
            addMonthField(height);
            addYearField(height);
        }

        private double addDayField(double left, double top)
        {
            m_canvas.DrawString(left, top, "<< ");

            PdfTextBox dayTextBox = m_page.AddTextBox(
                "day",
                m_canvas.TextPosition,
                m_canvas.MeasureText("00")
            );
            setupTextBox(dayTextBox, m_canvas.Font);
            dayTextBox.MaxLength = 2;
            dayTextBox.OnKeyPress = getValidateNumericAction();

            m_canvas.TextPosition = new PdfPoint(
                m_canvas.TextPosition.X + dayTextBox.Width,
                m_canvas.TextPosition.Y
            );

            m_canvas.DrawString(" >> ");
            return dayTextBox.Height;
        }

        private void addMonthField(double height)
        {
            double width = double.MinValue;
            foreach (string month in Months)
            {
                double monthWidth = m_canvas.GetTextWidth("   " + month + "   ");
                if (monthWidth > width)
                    width = monthWidth;
            }

            m_canvas.CurrentPosition = new PdfPoint(m_canvas.TextPosition.X, m_canvas.TextPosition.Y + height);
            m_canvas.DrawLineTo(m_canvas.TextPosition.X + width, m_canvas.TextPosition.Y + height);

            PdfTextBox monthTextBox = m_page.AddTextBox("month", m_canvas.TextPosition, new PdfSize(width, height));
            setupTextBox(monthTextBox, m_canvas.Font);

            m_canvas.TextPosition = new PdfPoint(m_canvas.TextPosition.X + width, m_canvas.TextPosition.Y);
            m_canvas.DrawString("   ");
        }

        private void addYearField(double height)
        {
            double width = m_canvas.GetTextWidth("0000");

            m_canvas.CurrentPosition = new PdfPoint(m_canvas.TextPosition.X, m_canvas.TextPosition.Y + height);
            m_canvas.DrawLineTo(m_canvas.TextPosition.X + width, m_canvas.TextPosition.Y + height);

            PdfTextBox yearTextBox = m_page.AddTextBox("year", m_canvas.TextPosition, new PdfSize(width, height));
            setupTextBox(yearTextBox, m_canvas.Font);
            yearTextBox.MaxLength = 4;
            yearTextBox.OnKeyPress = getValidateNumericAction();
        }

        private static void setupTextBox(PdfTextBox textBox, PdfFont font)
        {
            textBox.Font = font;
            textBox.FontSize = FontSize;
            textBox.HasBorder = false;
            textBox.TextAlign = PdfTextAlign.Center;
        }

        private PdfJavaScriptAction getValidateNumericAction()
        {
            if (m_validateNumeric == null)
                m_validateNumeric = m_document.CreateJavaScriptAction(Resources.ValidateNumeric);

            return m_validateNumeric;
        }
    }
}
