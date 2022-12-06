namespace mantenimiento_api.Controllers.RR
{
    public class ApiResponseBase <T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public void Successful()
        {
            this.Status = true;
            this.Message = string.Empty;
        }

        public void Error(string error)
        {
            this.Status = false;
            this.Message = error;
        }
    }
}
