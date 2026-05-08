namespace EMS.GenericResponse
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public string? Message { get; set; }
        public bool Ok { get; set; }

        public static ServiceResponse<T> Success(T? data, string message)
        {
            return new ServiceResponse<T> { Data = data, Message = message, Ok = true };
        }
        public static ServiceResponse<T> Failure(T? data, string message)
        {
            return new ServiceResponse<T> { Data = data, Message = message, Ok = false };
        }
    }
}
