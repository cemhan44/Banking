using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Domain.SeedWork
{
    public abstract class BaseEntity
    {
	    [Required]
        public Int32 Id { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
    }
}
