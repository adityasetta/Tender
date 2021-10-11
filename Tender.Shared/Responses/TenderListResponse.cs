namespace Tender.Shared.Responses
{
    using System;

    public class TenderListResponse
    {
        public long TenderId { get; set; }
        public string ContractNo { get; set; }
        public string TenderName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
