namespace BookCollect.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public string MessageTemplate { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;  
        public DateTime TimeStamp { get; set; }
        public string Properties { get; set; } = string.Empty; //XML properties
        public string Exception { get; set; } = string.Empty;   
        public string LogEvent { get; set; } = string.Empty;   

    }
}
