namespace Zomato.Dto
{
    public class SmtpEmailDetailsDto
    {
        public String recipient { get; set; }
        public String msgBody { get; set; }
        public String subject { get; set; }
        public String attachment { get; set; }
    }
}
