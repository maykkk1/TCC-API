namespace Gerenciador.Service.Common;

public class ServiceResult<T>
{
    public bool Success { get; set; } = true;
    public T Data { get; set; }
    public string ErrorMessage { get; set; }
}