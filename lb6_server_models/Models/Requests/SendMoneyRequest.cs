namespace lb6_server.Models.Requests
{
    public class SendMoneyRequest
    {
        public string InititalUserId { get; set; }
        public string RequestUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
