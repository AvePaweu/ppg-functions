namespace FunctionApp1
{
    public class Position {
        public string id { get; set; }
        public string label { get; set; }
        public string lineLabel { get; set; }
        public decimal lat { get; set; }
        public decimal lon { get; set; }
        public byte time { get; set; }
        public long depid { get; set; }
        public string type { get; set; }
    }
}