namespace NetCoreBoilerplate.Application.Common.Models
{
    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class Email<T> : Email where T : class
    {
        public string Template { get; set; }
        public T Model { get; set; }
    }
}
