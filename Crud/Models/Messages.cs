using System;
using System.Collections.Generic;

namespace Crud.Models
{
    public partial class Messages
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
