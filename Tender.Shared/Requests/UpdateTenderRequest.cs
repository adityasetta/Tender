namespace Tender.Shared.Requests
{
    using System;

    public class UpdateTenderRequest
    {
        public long TenderId { get; set; }
        public string ContractNo { get; set; }
        public string TenderName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Description { get; set; }
    }
}
