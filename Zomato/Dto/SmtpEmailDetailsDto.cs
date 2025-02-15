namespace Zomato.Dto
{
    public class SmtpEmailDetailsDto
    {
        private String recipient { get; set; }
        private String msgBody { get; set; }
        private String subject { get; set; }
        private String attachment { get; set; }
    }
}
