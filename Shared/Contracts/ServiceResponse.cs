namespace Mardev.Arq.Shared.Contracts
{
    public class ServiceResponse<T>
    {
        public T? Result { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "";
    }
}
