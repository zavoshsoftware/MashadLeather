using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class KiyanLog
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime LogDate { get; set; }

        public int Count { get; set; }
        public int? InventoryId { get; set; }

        public string FileUrl { get; set; }
        public bool IsSuccess { get; set; }
    }
}