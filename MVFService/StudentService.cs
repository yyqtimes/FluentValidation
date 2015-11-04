using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using MVFModel;
using System.Collections.ObjectModel;
namespace MVFService
{

    public class StudentService
    {
        public bool Insert(StudentModel mdoel)
        {
            using (var conn = DataBaseConnection.GetSqlserverConnection())
            {
                return conn.Insert(mdoel);
            }
        }
        public List<StudentModel> GetStudentList()
        {
            string strsql = "select * from students";
            using (var conn = DataBaseConnection.GetSqlserverConnection())
            {
                return conn.Query<StudentModel>(strsql).ToList();
                //return conn.GetList<StudentModel>().ToList();
            }
        }
        public StudentModel GetModel(int Id)
        {
            using (var conn=DataBaseConnection.GetSqlserverConnection())
            {
                return conn.Get<StudentModel>(new { Id = Id });
            }

        }
        public bool Update(StudentModel model)
        {
             using (var conn = DataBaseConnection.GetSqlserverConnection())
            {
                return conn.Update(model);
            }
        }
    }
}
