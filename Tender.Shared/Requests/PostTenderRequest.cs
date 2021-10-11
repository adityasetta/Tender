namespace Tender.Shared.Requests
{
    using System;

    public class PostTenderRequest
    {
        public string ContractNo { get; set; }
        public string TenderName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string Description { get; set; }
    }
}
