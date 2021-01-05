using Capstone.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Infrastructure.Utils
{
    public class Manage
    {
        public List<Child> list { get; set; }
        public Manage()
        {
            list = new List<Child>();
        }
        public void add(int? newEle, CategoryDto data)
        {
            if (data != null)
            {
                Child child = getItem(newEle);
                if (child != null)
                {
                    child.addRecord(data);
                }
                else
                {
                    Child tmp = new Child(newEle);
                    tmp.addRecord(data);
                    list.Add(tmp);
                }
            }

        }

        public Child getItem(int? newEleID)
        {
            foreach (var item in list)
            {
                if (item.Id == newEleID)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
