namespace 贵州省干部在线学习助手
{
    using System;
    using System.Data;

    internal class CourseDAL
    {
        public static int AddCourse(int id, string name, bool isRequire, bool isExam, DateTime addTime, DateTime learningTime, double credit)
        {
            return AccessHelper.ExecuteSql(string.Concat(new object[] { "insert into [Course] values(", id, ",'", name, "',", isRequire, ",", isExam, ",'", addTime, "','", learningTime, "',", credit, ")" }));
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

