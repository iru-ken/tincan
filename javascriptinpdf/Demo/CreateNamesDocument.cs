using BitMiracle.Docotic.Pdf;

namespace JavascriptInPdf
{
    static class CreateNamesDocument
    {
        private const double LeftMargin = 80; // the width of the left margin of the document
        private const double TopMargin = 50; // the height of the top margin of the document
        private const double RightMargin = 50; // the width of the right margin of the document

        private const double FontSize = 10; // font size in the document
        private const double LoremIpsumFontSize = 7; // font size of the lorem ipsum part

        public static void Run()
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages[0];

            PdfCanvas canvas = page.Canvas;
            canvas.FontSize = FontSize;

            drawNameField(canvas, 40, page, 0);
            drawLoremIpsum(canvas, page.Width);
            drawNameField(canvas, 200, page, 1);

            document.Save("Names.pdf");
            System.Diagnostics.Process.Start("Names.pdf");
        }

        private static void drawNameField(PdfCanvas canvas, double offsetFromTopMargin, PdfPage page, int nameFieldIndex)
        {
            const string caption = "First name:";

            double left = LeftMargin;
            double top = TopMargin + offsetFromTopMargin;
            canvas.DrawString(left, top, caption);

            PdfSize captionSize = canvas.MeasureText(caption);
            double width = page.Width - RightMargin - LeftMargin - captionSize.Width;

            PdfTextBox textBox = page.AddTextBox("name" + nameFieldIndex, canvas.TextPosition.X, canvas.TextPosition.Y, width, captionSize.Height);
            textBox.Font = canvas.Font;
            textBox.FontSize = FontSize;
            textBox.ShowBorder = false;
            textBox.HasBorder = false;
            textBox.TextAlign = PdfTextAlign.Center;
        }

        private static void drawLoremIpsum(PdfCanvas canvas, double pageWidth)
        {
            double fontSizeBefore = canvas.FontSize;
            try
            {
                PdfRectangle loremIpsumBounds = new PdfRectangle(
                    LeftMargin,
                    TopMargin + 75,
                    pageWidth - RightMargin - LeftMargin,
                    150
                );
                canvas.FontSize = LoremIpsumFontSize;
                canvas.DrawText(Resources.LoremIpsum, loremIpsumBounds, PdfTextAlign.Left, PdfVerticalAlign.Top);
            }
            finally
            {
                canvas.FontSize = fontSizeBefore;
            }
        }
    }
}
