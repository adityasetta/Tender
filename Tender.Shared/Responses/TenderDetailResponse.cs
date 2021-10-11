namespace Tender.Shared.Responses
{
    using System;

    public class TenderDetailResponse
    {
        public long TenderId { get; set; }
        public string ContractNo { get; set; }
        public string TenderName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
    }
}
