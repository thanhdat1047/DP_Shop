namespace DP_Shop.DTOs.Result
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }

        // Thanh cong
        public Result(T data)
        {
            Succeeded = true;
            Data = data;
            ErrorMessage = string.Empty;
        }

        // That bai
        public Result(string errorMessage)
        {
            Succeeded = false;
            Data = default(T);
            ErrorMessage = errorMessage;
        }


    }
}
