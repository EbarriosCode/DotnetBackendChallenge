namespace Application.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, IDictionary<string, string[]> errors) : base(message)
        {
            this.Errors = errors;
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}