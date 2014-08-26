using BitMiracle.Docotic.Pdf;

namespace JavascriptInPdf
{
    static class Names
    {
        public static void Run()
        {
            PdfDocument pdf = new PdfDocument();
            pdf.Open(Resources.NamesPdf);

            pdf.SharedScripts.Add(
                pdf.CreateJavaScriptAction(Resources.SynchronizeFields)
            );

            pdf.GetControl("name0").OnLostFocus = pdf.CreateJavaScriptAction("synchronizeFields(\"name0\", \"name1\");");
            pdf.GetControl("name1").OnLostFocus = pdf.CreateJavaScriptAction("synchronizeFields(\"name1\", \"name0\");");

            pdf.Save("NamesModified.pdf");
            System.Diagnostics.Process.Start("NamesModified.pdf");
        }
    }
}
