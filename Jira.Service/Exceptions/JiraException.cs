namespace Jira.Service.Exceptions
{
    public class JiraException : Exception
    {
        public int Code { get; set; }
        public JiraException(int code, string message = "something went wrong") : base(message)
        {
            this.Code = code;
        }
    }
}
