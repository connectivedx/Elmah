using System;
namespace Elmah
{
	partial class ThrottledErrorMailHtmlPage
	{
		public Error Error { get; private set; }
		public int ErrorCount { get; private set; }

		public ThrottledErrorMailHtmlPage(Error error, int errorCount)
        {
            if (error == null) throw new ArgumentNullException("error");
            Error = error;
			ErrorCount = errorCount;
        }
	}
}