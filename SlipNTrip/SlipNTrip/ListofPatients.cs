using SlipNTrip.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;

namespace SlipNTrip
{
    class ListofPatients
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "patients.db3");

        public void GenerateListofPatients()
        {
            var db = new SQLiteConnection(dbPath);
            db.CreateTable<Patient>();
            db.DeleteAll<Patient>();
            Random rnd = new Random();
            
            string[] patientGender = { "Male","Female","Male","Male","Female",
                                        "Female","Male","Male","Male","Male",
                                        "Male","Male","Male","Female","Male",
                                        "Male","Male","Female","Male","Female",
                                        "Male","Female","Female","Male","Male",
                                        "Female","Female","Female","Female","Male",
                                        "Female","Male","Male","Male","Male",
                                        "Male","Male","Female","Female","Male",
                                        "Male","Female","Male","Female","Male",
                                        "Male","Female","Male","Female","Female"};
            string[] patientName = {"Patrick Farrell","Evelyn Williams","Chester Williams","Andrew Elliott","Melanie Crawford",
                                    "Isabella Harrison","Arnold Armstrong","Fenton Payne","Preston Bennett","Garry Campbell",
                                    "Marcus Baker","Justin Foster","James Watson","Catherine Stewart","Arnold Perry",
                                    "Stuart Alexander","Paul Johnston","Chelsea Morgan","Adam Harris","Savana Ferguson",
                                    "Edgar Harris","Tess Wells","Lilianna Dixon","Aldus Tucker","Abraham Harper",
                                    "Olivia Holmes","Aida Owens","Lucia Williams","Cherry Higgins","David Elliott",
                                    "Rebecca Myers","Martin Smith","John Ryan","John Farrell","Maximilian Johnson",
                                    "Max Richards","Dale Baker","Kelsey Robinson","Brooke Riley","Adrian Cunningham",
                                    "Oliver Stevens","Abigail Adams","Paul Perry","Chelsea Roberts","Michael Miller",
                                    "James Hunt","Valeria Hill","Kristian Williams","Victoria Anderson","Gianna Alexander"};
            string tempPatientID;
            for (int i = 0; i < 50; i++)
            {
                if(i + 1< 10)
                {
                    tempPatientID = "M_00" + (i+1);
                }
                else
                    tempPatientID = "M_0" + (i+1);
                Patient patient = new Patient()
                {
                    PatientID = tempPatientID,
                    Name = patientName[i],
                    Gender = patientGender[i],
                    Age = rnd.Next(50, 80),
                    Height = Math.Round(rnd.Next(5,6) + rnd.NextDouble(), 1),
                    Weight = Math.Round(rnd.Next(120, 300) + rnd.NextDouble(),2),
                    ShoeSize = Math.Round(rnd.Next(6,12) + rnd.NextDouble(),1)
                };

                db.Insert(patient);
            }
           
        }
    }
}
