namespace DasignoTest.AuxModels.AuxResponseModels
{
    public record CommonResponse<T>
    {
        public int status {  get; set; }
        public string message { get; set; }
        public T? data { get; set; }
        public int? totalRecords { get; set; }
        public int? totalPages { get; set; }
    }
}
