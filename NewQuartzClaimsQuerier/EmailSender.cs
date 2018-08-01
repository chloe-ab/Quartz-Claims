using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using Newtonsoft.Json;


namespace NewQuartzClaimsQuerier
{
    class EmailSender
    {
        private static readonly string EMAIL_SETTINGS_FILE_PATH = "NewQuartzClaimsQuerier.resources.emailSettings.settings.json";

        public static void SendEmail(XLWorkbook claimsWorkbookToSend, List<string> fullClaimsTableNames)
        {
            Dictionary<string, string> emailSettings = GetEmailSettings();
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(emailSettings["smtpClientName"]);
            mail.From = new MailAddress(emailSettings["senderAddress"]);
            mail.To.Add(emailSettings["recipientAddress"]);
            mail.Subject = "Filtered Yukon Claims " + DateTime.Now.ToShortDateString();

            mail.Body = "See attached Excel workbook for Yukon claims filtered by: " + String.Join(", " , fullClaimsTableNames) + "\n";  //body content
            Attachment attachment = new Attachment(ExcelWriter.WorkbookPath);  //file path to workbook
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailSettings["senderAddress"], emailSettings["senderPassword"]);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
            Console.WriteLine("Successfully sent email");
        }

        public static Dictionary<string, string> GetEmailSettings()
        {
            string fileText;
            Assembly _assembly = Assembly.GetExecutingAssembly();

            using (var streamReader = new StreamReader(_assembly.GetManifestResourceStream(EMAIL_SETTINGS_FILE_PATH)))
            {
                fileText = streamReader.ReadToEnd();
            }
            Dictionary<string, string> root = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileText);

            return root;
        }
    }
}
