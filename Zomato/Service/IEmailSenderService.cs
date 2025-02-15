using Zomato.Dto;

namespace Zomato.Service
{
    public interface IEmailSenderService
    {
        String sendSimpleMail(SmtpEmailDetailsDto SmtpEmailDetails);

        String sendMailWithAttachment(SmtpEmailDetailsDto SmtpEmailDetails);
    }
}
