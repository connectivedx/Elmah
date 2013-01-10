using System;
namespace Elmah
{
	public class ThrottledErrorMailHtmlFormatter : ErrorTextFormatter
	{
		public override void Format(System.IO.TextWriter writer, Error error)
		{
			Format(writer, error, 1);
		}

		public void Format(System.IO.TextWriter writer, Error error, int errorCount)
		{
			if (writer == null) throw new ArgumentNullException("writer");
			if (error == null) throw new ArgumentNullException("error");

			var page = new ThrottledErrorMailHtmlPage(error, errorCount);
			writer.Write(page.TransformText());
		}

		public override string MimeType
		{
			get { return "text/html"; }
		}
	}
}