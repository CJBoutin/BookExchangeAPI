namespace TextbookTradingServiceLayer.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bid
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TransactionId { get; set; }

        public int ProposedPrice { get; set; }

        public int? Accepted { get; set; }

        public virtual Transaction Transaction { get; set; }

        public virtual User User { get; set; }
    }
}
