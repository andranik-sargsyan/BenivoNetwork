namespace BenivoNetwork.Models
{
    public class ResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        public ResponseModel()
        {

        }

        public ResponseModel(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }

        public ResponseModel()
        {

        }

        public ResponseModel(bool isSuccessful, string message, T data) : base(isSuccessful, message)
        {
            Data = data;
        }
    }
}