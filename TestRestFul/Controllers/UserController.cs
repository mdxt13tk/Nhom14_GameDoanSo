using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestRestFul.Controllers
{
    public class UserController : ApiController
    {
        public static ArrayList userOnline = new ArrayList();
        [HttpGet]   // Dịch vụ GET
        //Lấy thông tin tất cả user
        public List<User> GetUserLists()
        {
            DBGameDataContext db = new DBGameDataContext();
            return db.Users.ToList();
        }

        [HttpGet]
        //[Route("api/user/{id}")]
        //Lấy thông tin theo id 
        public User GetUser(string id)
        {
            DBGameDataContext db = new DBGameDataContext();

            return db.Users.FirstOrDefault(x => x.IDUser == id);
        }

        [HttpPost]
        [Route("api/user/logout")]
        public bool logout(User userFromClient)
        {
            DBGameDataContext db = new DBGameDataContext();

            User usr = db.Users.FirstOrDefault(x => x.IDUser == userFromClient.IDUser);
            if(usr != null)
            {
                userOnline.Remove(usr.IDUser);
                return true;
            }
            else return false;
        }

        [HttpPost]
        [Route("api/user/login")]
        public string login(User userFromClient)
        {
            DBGameDataContext db = new DBGameDataContext();
            
            User usr = db.Users.FirstOrDefault(x => x.IDUser == userFromClient.IDUser);
            if (usr == null) return "IdError";
            else if(usr.MK.Trim() != userFromClient.MK.Trim())
            {
                return "PassError";
            }
            else
            {
                userOnline.Add(usr.IDUser);
                return usr.HoTen;
            }
        }

        [HttpPost]      //Dịch vụ POST
        [Route("api/user/register")]
        public bool InsertNewUser(User userFromClient)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                User us = new User();
                us.IDUser = userFromClient.IDUser;
                us.MK = userFromClient.MK;
                us.HoTen = userFromClient.HoTen;
                db.Users.InsertOnSubmit(us);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPut]      //Dịch vụ PUT
        [Route("api/user/updatemk")]
        public bool UpdateUserMK(User user)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                User us = db.Users.FirstOrDefault(x => x.IDUser == user.IDUser);
                if (us == null) return false;//không tồn tại false
                us.MK = user.MK;
                db.SubmitChanges();//xác nhận chỉnh sửa
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]      //Dịch vụ PUT
        [Route("api/user/update")]
        public bool UpdateUser(User user)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                User us = db.Users.FirstOrDefault(x => x.IDUser == user.IDUser);
                if (us == null) return false;//không tồn tại false
                us.MK = user.MK;
                us.HoTen = user.HoTen;
                db.SubmitChanges();//xác nhận chỉnh sửa
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteUser(string id)
        {
            DBGameDataContext db = new DBGameDataContext();
            //lấy user tồn tại ra
            User us = db.Users.FirstOrDefault(x => x.IDUser == id);
            if (us == null) return false;
            db.Users.DeleteOnSubmit(us);
            db.SubmitChanges();
            return true;
        }

    }
}
