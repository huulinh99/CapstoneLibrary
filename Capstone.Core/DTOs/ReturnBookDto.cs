using Capstone.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Core.DTOs
{
    public class ReturnBookDto : IEquatable<ReturnBookDto>
    {
        public int Id { get; set; }
        public int? PatronId { get; set; }
        public string PatronName { get; set; }
        public string StaffName { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public float Fee { get; set; }
        public DateTime? ReturnTime { get; set; }
        public int? BorrowId { get; set; }
        public int? StaffId { get; set; }
        public virtual ICollection<ReturnDetailDto> ReturnDetail { get; set; }

        public bool Equals(ReturnBookDto other)
        {
            return (ReturnTime.Value.ToString("MMMM yyyy").Equals(other.ReturnTime.Value.ToString("MMMM yyyy")));
        }

        public override int GetHashCode()
        {
            int hashReturnTime= ReturnTime == null ? 0 : ReturnTime.GetHashCode();
            return hashReturnTime;
        }
    }
}
