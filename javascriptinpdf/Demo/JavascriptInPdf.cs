using BitMiracle.Docotic.Pdf;

namespace JavascriptInPdf
{
    public static class Demo
    {
        public static void Main(string[] args)
        {
            //helloWorld();
            //formWithDate();
            synchronizeNameFields();
        }

        private static void helloWorld()
        {
            using (PdfDocument pdf = new PdfDocument())
            {
                pdf.OnOpenDocument = pdf.CreateJavaScriptAction("app.alert(\"Hello CodeProject!\", 3);");

                pdf.Save("Hello World.pdf");
                System.Diagnostics.Process.Start("Hello World.pdf");
            }
        }

        private static void formWithDate()
        {
            new Date().Run();
        }

        private static void synchronizeNameFields()
        {
            Names.Run();
        }
    }
}
