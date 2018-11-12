using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmallBusiness.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(50)]
        public string Phone { get; set; }
        [Required]
        public int GenderId { get; set; }
        public int CityId { get; set; }
        public int RegionId { get; set; }
        [DataType(DataType.Date)]
        public DateTime LastPurchase { get; set; }
        public int ClassificationId { get; set; }
        public int UserId { get; set; }

        public Gender Gender { get; set; }
        public City City { get; set; }
        public Region Region { get; set; }
        public Classification Classification { get; set; }
        public User User { get; set; }
    }
}