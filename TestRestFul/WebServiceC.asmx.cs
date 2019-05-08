using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TestRestFul
{
    /// <summary>
    /// Summary description for WebServiceC
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceC : System.Web.Services.WebService
    {

        DBGameDataContext db = null;
        public WebServiceC()
        {

            //Uncomment the following line if using designed components
            //InitializeComponent();
            db = new DBGameDataContext();
        }

        //Hàm đếm xem có bao nhiêu user
        [WebMethod]
        public int CountUser()
        {
            db = new DBGameDataContext();
            return db.Users.Count();
        }

        //2- Hàm trả về danh sách user
        [WebMethod]
        public List<User> GetUserLists()
        {
            db = new DBGameDataContext();
            return db.Users.ToList();
        }

        [WebMethod]
        //Lấy thông tin theo id
        public User GetUser(string id)
        {
            db = new DBGameDataContext();
            return db.Users.FirstOrDefault(x => x.IDUser.Trim() == id);
        }


        [WebMethod]
        // Thêm 1 user mới
        public bool InsertNewUser(string id, string mk, string ten)
        {
            try
            {
                db = new DBGameDataContext();
                User us = new User();
                us.IDUser = id;
                us.MK = mk;
                us.HoTen = ten;
                db.Users.InsertOnSubmit(us);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [WebMethod]
        public bool UpdateUser(string id, string mk, string ten)
        {
            try
            {
                db = new DBGameDataContext();
                //lấy user trong database ra
                User us = db.Users.FirstOrDefault(x => x.IDUser == id);
                if (us == null) return false;//không tồn tại false
                us.MK = mk;
                us.HoTen = ten;
                db.SubmitChanges();//xác nhận chỉnh sửa
                return true;
            }
            catch
            {
                return false;
            }
        }

        [WebMethod]
        //Xóa 1 user
        public bool DeleteUser(string id)
        {
            db = new DBGameDataContext();
            //lấy user tồn tại ra
            User us = db.Users.FirstOrDefault(x => x.IDUser == id);
            if (us == null) return false;
            db.Users.DeleteOnSubmit(us);
            db.SubmitChanges();
            return true;
        }

    }
}
