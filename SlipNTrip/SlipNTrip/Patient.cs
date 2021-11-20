using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SlipNTrip
{
    public class Patient
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string PatientID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double ShoeSize { get; set; }

        public override string ToString()
        {
            return this.PatientID;
        }
    }

    
}
