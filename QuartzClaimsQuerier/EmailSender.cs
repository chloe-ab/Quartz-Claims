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

        public static void SendEmail(XLWorkbook claimsWorkbookToSend, List<string> fullClaimsTableNames, string attachmentName)
        {
            //Console.WriteLine("pre email");
            Dictionary<string, string> emailSettings = GetEmailSettings();
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(emailSettings["smtpClientName"]);
            mail.From = new MailAddress(emailSettings["senderAddress"]);
            mail.To.Add(emailSettings["recipientAddress"]);
            mail.To.Add(emailSettings["recipientAddress2"]);
            mail.Subject = "Filtered Yukon Claims " + DateTime.Now.ToShortDateString();

            mail.Body = "See attached Excel workbook for Yukon claims filtered by: " + String.Join(", " , fullClaimsTableNames) + "\n";  //body content

            //claimsWorkbookToSend.SaveAs(@"C:/Users/Chloe/Desktop/TestExcel.xlsx"); //for testing
            Stream stream = new MemoryStream();
            claimsWorkbookToSend.SaveAs(stream);
            stream.Position = 0;  //needed to set stream position to beginning as opposed to end
            Attachment attachment = new Attachment(stream, attachmentName);
            //stream.Close();
            mail.Attachments.Add(attachment);

            //below line added Aug 28:
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            //NOTE THAT THE ABOVE LINE DOESN'T AFFECT THE ERROR OF SMTP SERVER ON AZURE; IT HAPPENED WITH AND WITHOUT THE ABOVE LINE.
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailSettings["senderAddress"], emailSettings["senderPassword"]);
            //default credentials are false which is what we want
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
