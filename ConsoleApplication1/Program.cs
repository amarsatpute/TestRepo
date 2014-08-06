using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
           // Console.WriteLine("test");
           // Console.ReadLine();
            var exams = new Dictionary<string, Patient>();

            var con = new SqlConnection();
            con.ConnectionString = "Data Source=sg-us-sql1-dev;Initial Catalog=OM_Nate;Integrated Security=True;";
            using (con)
            {
                var sql = "SELECT P.fname,P.Lname,PE.ExamDate FROM PATIENT P LEft outer join patientexam PE on P.PatientID = PE.PatientID ";
                var patient = new Patient();
                var patients = con.Query<Patient, PatientExam, Patient>(sql, (P, PE) =>
                {
                         Patient _patient;
                         if (!exams.TryGetValue(P.PatientID, out _patient))
                         {
                             exams.Add(P.PatientID, _patient = P);
                         }
                         if (_patient.lstExams == null)
                             _patient.lstExams = new List<PatientExam>();
                         _patient.lstExams.Add(PE);
                         return _patient;
                     }
                     ).AsQueryable();
                var resultList = exams.Values;


            }

        }


    }

    public class Patient
    {
        public string ClinicID { get; set; }
        public string PracticeID { get; set; }
        public string PatientID { get; set; }
        public string FName { get; set; }
        public string Lname { get; set; }
        public string MName { get; set; }

        public string Title { get; set; }
        public DateTime? DOB { get; set; }

        public string ProviderID { get; set; }
        public DateTime? NextApptDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }

        public long? HomePhone { get; set; }

        public long? CellPhone { get; set; }

        public long? WorkPhone { get; set; }
        public string WorkPhoneExt { get; set; }

        public string InsuranceID { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string MRN { get; set; }
        public string PRIAM { get; set; }
        public string OSMCenter { get; set; }

        public string PriorStatus { get; set; }
        public string AddressNotes { get; set; }
        public string UserDefinedField1 { get; set; }
        public string UserDefinedField2 { get; set; }

        public string PracticeOffice { get; set; }

        public string RecallType { get; set; }
        public DateTime? RecallDate { get; set; }
        public string IsReminderLetterSent { get; set; }
        public string IsReminderEmailSent { get; set; }
        public bool IsPrint { get; set; }
        public string IsRecallLetterSent { get; set; }
        public int? RecordedByID { get; set; }

        public DateTime? RecordedDate { get; set; }
        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string InactiveYN { get; set; }
        public string DeleteYN { get; set; }

        public List<PatientExam> lstExams { get; set; }

    }

    public class PatientExam
    {
        public string PatientID { get; set; }
        public string PatientExamID { get; set; }
        public DateTime? ExamDate { get; set; }

        public string CPTCodeID { get; set; }

        public string TechnologistsID { get; set; }

        public string Insurance { get; set; }

        public string ProviderID { get; set; }
        public string IsExamComplete { get; set; }
        public string Remarks { get; set; }

        public string Recall { get; set; }

        public string PracticeID { get; set; }

        public int? RecordedByID { get; set; }

        public DateTime? RecordedDate { get; set; }
        public int? ModifiedByID { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string InactiveYN { get; set; }
        public string DeleteYN { get; set; }

    }

}
