using System.Collections.Generic;

namespace DemoData
{
    public class Hospital
    {
        readonly int id;
        string name;
        string city;
        public int Id { get { return id; } }
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
        public Hospital(int id, string name, string city)
        {
            this.id = id;
            this.name = name;
            this.city = city;
        }
        Hospital() { }
        public static List<Hospital> GetSampleHospitals()
        {
            return new List<Hospital>
                       {
                           new Hospital(id:1, name: "Crouse Hospital", city:"Oklahoma City"),
                           new Hospital(id:2, name: "AHMC Anaheim Regional Medical Center", city:"Broken Arrow"),
                           new Hospital(id:3, name: "St. Luke", city:"Oklahoma City"),
                           new Hospital(id:4, name: "AdventHealth Wesley Chapel", city:"Midwest City"),
                           new Hospital(id:5, name: "SSM Health St. Anthony Healthplex", city:"Oklahoma City"),
                           new Hospital(id:6, name: "St. George", city:"Norman"),
                           new Hospital(id:7, name: "CJW Medical Center - Johnston-Willis Campus", city:"Tahlequah"),
                           new Hospital(id:8, name: "Barnes Jewish", city:"Tulsa"),
                           new Hospital(id:9, name: "Crouse Hospitalt", city:"Mustang"),
                           new Hospital(id:10, name: "Curahealth Oklahoma City", city:"Moore")
                       };
        }
    }
}
