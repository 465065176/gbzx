namespace 贵州省干部在线学习助手
{
    using System;
    using System.Data;

    internal class ExamsDAL
    {
        public static int AddExam(int CourseId, string ExamTitle, string ExamItem, int ExamType, string ExamAnswer ,bool ExamAnswerVerify, DateTime AddTime)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "insert into [Exams] (CourseId,ExamTitle,ExamItem,ExamType,ExamAnswer,ExamAnswerVerify,AddTime) values(", CourseId, ",'", ExamTitle, "','", ExamItem, "',", ExamType, ",'", ExamAnswer, "',", ExamAnswerVerify, ",'", AddTime, "')" }));
        }
        public static int UpExam(int id, string ExamAnswer)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "UPDATE [Exams] SET ExamAnswer='", ExamAnswer, "',ExamAnswerVerify=false", " WHERE (ID=", id, ");" }));
        }
        public static int UpExam(int id, string ExamAnswer, bool ExamAnswerVerify)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "UPDATE [Exams] SET ExamAnswer='", ExamAnswer, "',ExamAnswerVerify=",ExamAnswerVerify," WHERE (ID=", id, ");" }));
        }
        public static int UpExam(int id, bool ExamAnswerVerify)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "UPDATE [Exams] SET ExamAnswerVerify=", ExamAnswerVerify, " WHERE (ID=", id, ");" }));
        }
        public static int UpExam(string ExamTitle, bool ExamAnswerVerify)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "UPDATE [Exams] SET ExamAnswerVerify=", ExamAnswerVerify, " WHERE (ExamTitle='", ExamTitle, "');" }));
        }
        public static DataTable getExams(int CourseId)
        {
            return AccessHelper.DataTable("SELECT * FROM Exams where CourseId=" + CourseId);

        }
        public static DataTable getExams(string ExamTitle)
        {
            return AccessHelper.DataTable("SELECT * FROM Exams where ExamTitle='" + ExamTitle+"'");
        }
        public static DataTable getExams(string ExamTitle,int CourseId)
        {
            return AccessHelper.DataTable("SELECT * FROM Exams where ExamTitle='" + ExamTitle + "' and CourseId="+CourseId);
        }
        public static int DeleteAllUser()
        {
            string sqlstr = "delete from [User]";
            return AccessHelper.ExecuteSql(sqlstr);
        }

        public static DataTable getCourse(int Id)
        {
            return AccessHelper.DataTable("SELECT top 1 * FROM Course where id<" + Id + " order by id desc");
        }

        public static int UpUserGZ(string uid, bool gz)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "UPDATE [User] SET gz=", gz, " WHERE (uid='", uid, "');" }));
        }

        public static int UpUserInfo(string uid, string info)
        {
            return AccessHelper.ExecuteSql("UPDATE [User] SET info='" + info + "' WHERE (uid='" + uid + "');");
        }

        public static int UpUserPL(string uid, bool pl)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "UPDATE [User] SET pl=", pl, " WHERE (uid='", uid, "');" }));
        }

        public static int UpUserZ(string uid, bool z)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "UPDATE [User] SET z=", z, " WHERE (uid='", uid, "');" }));
        }
    }
}

