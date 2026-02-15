namespace ScrapDealer.Shared.Abstractions.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message): base(message) 
        {
            
        }
    }
}
