namespace CapaNegocio
{
    public class DefaultResponse
    {
        public object? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int TotalRecords { get; set; }
    }
}
