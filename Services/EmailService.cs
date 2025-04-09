using MailKit.Net.Smtp;
using MimeKit;
using System.Net.Mail;

namespace BuildMasterPro.Services
{
    public class EmailService
    {
        string OTPstring;
        string email;
        private string OTP()
        {
            string otpSource = "1234567890abcdefghijklmnopqrstuvwxyz";
            Random rnd = new Random();
            string otp = "";

            for(int i = 0;i<6;i++)
            {
                int otpchar = rnd.Next(0, otpSource.Length + 1);
                otp += otpSource[otpchar];
            }

            return otp;
        }
        public async Task SendEmailOTP(string email)
        {
            string msg = OTP();
            this.OTPstring = msg;
            string senderEmail, senderPass, receiverEmail;
            receiverEmail = email;
            this.email = email;

            senderEmail = "philipjohn.segarra@gmail.com";
            senderPass = "igwv reig fphl yfnb";

            MimeMessage message = new MimeMessage(); // Creating object for Message
            message.From.Add(new MailboxAddress("BuilMasterPro", senderEmail)); //Sender's information
            message.To.Add(MailboxAddress.Parse(receiverEmail)); //Receiver's Information

            message.Subject = "One-Time-Password"; //Email's Subject

            //Email's Body
            message.Body = new TextPart("plain") //Plain text
            {
                Text = msg  //MSG = OTP
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, true); //Gmail's smtp server, PORT: 465
                    client.Authenticate(senderEmail, senderPass); //Login sender's email and password
                    client.Send(message); //
                }
                catch (Exception)
                {
                    
                    return;
                }
                finally
                {
                    client.Disconnect(true); // always Disconnect the service.
                    client.Dispose(); //Releases all resource used by the MailService object.
                }
            }
        }
        public async Task SendEmailNotif(string emailAddress, string subject, string content)
        {
            string senderEmail, senderPass, receiverEmail;
            receiverEmail = emailAddress;
            this.email = emailAddress;

            senderEmail = "philipjohn.segarra@gmail.com";
            senderPass = "igwv reig fphl yfnb";

            MimeMessage message = new MimeMessage(); // Creating object for Message
            message.From.Add(new MailboxAddress("BuilMasterPro", senderEmail)); //Sender's information
            message.To.Add(MailboxAddress.Parse(receiverEmail)); //Receiver's Information

            message.Subject = subject; //Email's Subject

            //Email's Body
            message.Body = new TextPart("plain") //Plain text
            {
                Text = content  //MSG = OTP
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 465, true); //Gmail's smtp server, PORT: 465
                    client.Authenticate(senderEmail, senderPass); //Login sender's email and password
                    client.Send(message); //
                }
                catch (Exception)
                {

                    return;
                }
                finally
                {
                    client.Disconnect(true); // always Disconnect the service.
                    client.Dispose(); //Releases all resource used by the MailService object.
                }
            }
        }

    }
}
