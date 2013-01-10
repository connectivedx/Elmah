using System.Collections.Generic;
using System;

namespace Elmah
{
	using System.IO;
	using System.Net.Mail;

	public class ThrottlingErrorMailModule : ErrorMailModule
	{
		static readonly Dictionary<string, Tuple<DateTime, int>> LogHistory = new Dictionary<string, Tuple<DateTime, int>>();
		static readonly object SyncRoot = new object();

		public ThrottlingErrorMailModule()
		{
			ThrottleInterval = 50;
			ThrottleMinutes = 10;
		}

		public int ThrottleInterval { get; set; }
		public int ThrottleMinutes { get; set; }

		protected virtual bool IsErrorCurrent(Error error)
		{
			var signature = error.UniqueId;

			Tuple<DateTime, int> errorState;
			if (!LogHistory.TryGetValue(signature, out errorState))
			{
				lock (SyncRoot)
				{
					if (!LogHistory.ContainsKey(signature))
						LogHistory.Add(signature, Tuple.Create(DateTime.Now, 1));
				}

				// error had not been logged previously, so it's current
				return true;
			}

			// if the error last occurred too long ago, resend email
			if (errorState.Item1 < DateTime.Now.AddMinutes(-ThrottleMinutes))
				return true;

			// if the error has occurred a multiple of the interval times, resend email
			if (errorState.Item2 % ThrottleInterval == 0)
				return true;

			return false;
		}

		protected virtual int GetErrorCount(Error error)
		{
			var signature = error.UniqueId;

			Tuple<DateTime, int> errorState;
			if (!LogHistory.TryGetValue(signature, out errorState))
			{
				return 0;
			}

			return errorState.Item2;
		}

		protected override void ReportError(Error error)
		{
			if (error == null)
				throw new ArgumentNullException("error");
			
			if (!IsErrorCurrent(error)) return;

			//
			// Start by checking if we have a sender and a recipient.
			// These values may be null if someone overrides the
			// implementation of OnInit but does not override the
			// MailSender and MailRecipient properties.
			//

			var sender = this.MailSender ?? string.Empty;
			var recipient = this.MailRecipient ?? string.Empty;
			var copyRecipient = this.MailCopyRecipient ?? string.Empty;

			if (recipient.Length == 0)
				return;

			//
			// Create the mail, setting up the sender and recipient and priority.
			//

			var mail = new MailMessage();
			mail.Priority = this.MailPriority;

			mail.From = new MailAddress(sender);
			mail.To.Add(recipient);

			if (copyRecipient.Length > 0)
				mail.CC.Add(copyRecipient);

			//
			// Format the mail subject.
			// 

			var subjectFormat = Mask.EmptyString(this.MailSubjectFormat, "Error ({1}): {0}");
			mail.Subject = string.Format(subjectFormat, error.Message, error.Type, GetErrorCount(error)).
				Replace('\r', ' ').Replace('\n', ' ');

			//
			// Format the mail body.
			//

			var formatter = new ThrottledErrorMailHtmlFormatter();

			var bodyWriter = new StringWriter();
			formatter.Format(bodyWriter, error, GetErrorCount(error));
			mail.Body = bodyWriter.ToString();

			switch (formatter.MimeType)
			{
				case "text/html": mail.IsBodyHtml = true; break;
				case "text/plain": mail.IsBodyHtml = false; break;

				default:
					{
						throw new ApplicationException(string.Format(
							"The error mail module does not know how to handle the {1} media type that is created by the {0} formatter.",
							formatter.GetType().FullName, formatter.MimeType));
					}
			}

			var args = new ErrorMailEventArgs(error, mail);

			try
			{
				//
				// If an HTML message was supplied by the web host then attach 
				// it to the mail if not explicitly told not to do so.
				//

				if (!NoYsod && error.WebHostHtmlMessage.Length > 0)
				{
					var ysodAttachment = CreateHtmlAttachment("YSOD", error.WebHostHtmlMessage);

					if (ysodAttachment != null)
						mail.Attachments.Add(ysodAttachment);
				}

				//
				// Send off the mail with some chance to pre- or post-process
				// using event.
				//

				OnMailing(args);
				SendMail(mail);
				OnMailed(args);
			}
			finally
			{
				OnDisposingMail(args);
				mail.Dispose();
			}
		}
	}
}