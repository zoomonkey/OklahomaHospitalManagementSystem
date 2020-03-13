using DemoData;
using System.Collections.Generic;

namespace OKHMS.Models
{
    public class HospitalModel
    {
        private int id;
        private string name;
        private string city;
        public int Id {
            get { return id; }   // get method
            set { id = value; }  // set method

        }
        public string Name   // property
        {
            get { return name; }   // get method
            set { name = value; }  // set method
        }
        public string City   // property
        {
            get { return city; }   // get method
            set { city = value; }  // set method
        }
        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }
        public List<Hospital> Hospitals { get; set; }
    }
}