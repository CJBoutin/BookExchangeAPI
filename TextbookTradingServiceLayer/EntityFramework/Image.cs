namespace TextbookTradingServiceLayer.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Image
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] ImageData { get; set; }

        public int UserId { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
