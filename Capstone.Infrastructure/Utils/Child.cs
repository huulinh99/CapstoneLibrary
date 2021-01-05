using Capstone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Utils
{
    public class Child
    {
        public int? Id { get; set; }
        public List<CategoryDto> listRecord { get; set; }
        public Child(int? id)
        {
            Id = id;
            listRecord = new List<CategoryDto>();
        }
        public void addRecord(CategoryDto record)
        {
            listRecord.Add(record);
        }
    }
}
