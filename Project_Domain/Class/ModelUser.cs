using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Common.Class;
using Project_Data.Class;
namespace Project_Domain.Class
{
    public class ModelUser
    {
        private CommandSelectUser? oSelectUser = null ;
        private CommandAddUser? oAddUser = null ;
        private CommandSelectInfoUsers ? oSelectInfoUser = null ;

        public ModelUser() { }
        public (bool,string, UserSession?) VerificationUser(string? User, string? Password)
        {
            oSelectUser = new(User, Password);
            return oSelectUser.LoginUser();
        }

        public (bool, string) VerificationRegisterUser(string Code, string User, string TypeUser, string FullName, string Password, string Email)
        {
            oAddUser = new(Code, User, TypeUser, FullName, Password, Email);
            return oAddUser.RegisterUser() ;
        }

        public (bool, string) VincularStudentUser(string FullNameStudent, string FullNameAdmin)
        {
            oAddUser = new();
            return oAddUser.InsertStudent(FullNameStudent,FullNameAdmin);
        }

        public (bool, string) VincularTeacherUser(string FullNameTeacher, string FullNameAdmin)
        {
            oAddUser = new();
            return oAddUser.InsertTeacher(FullNameTeacher, FullNameAdmin);
        }
        public  List<InfoUsers>? SelectInfoStudents(string FullNameAdmin)
        {
            oSelectInfoUser = new();
            return oSelectInfoUser.ExecuteInfoStudent(FullNameAdmin);
        }
        public List<InfoUsers>? SelectInfoTeachers(string FullNameAdmin)
        {
            oSelectInfoUser = new();
            return oSelectInfoUser.ExecuteInfoTeacher(FullNameAdmin) ;
        }
        public  (bool, string) SelectInfoMember(string User)
        {
            oSelectInfoUser = new();
           return oSelectInfoUser.SelectFullNameMember(User);
        }

        public  bool CheckStudentVinculate(string FullNameStudent)
        {
            oAddUser = new();
            return oAddUser.ExistsStudentBoundToAdmin(FullNameStudent);
        }
        public  bool CheckTeacherVinculate(string FullNameTeacher)
        {
            oAddUser = new();
            return oAddUser.ExistsTeacherBoundToAdmin(FullNameTeacher);
        }

        public List<string> ? SelectListAdmin()
        {
            oSelectInfoUser = new();
            return oSelectInfoUser.ExecuteListAdmin();
        }

    }
}
