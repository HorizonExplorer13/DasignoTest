namespace DasignoTest.AuxModels.AuxResponseModels
{
    public class requestResponse<T>
    {
        public string message { get; set; }
        public T data { get; set; }
     }
}
