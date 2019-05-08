using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestRestFul.Controllers
{
    public class DiemController : ApiController
    {
        [HttpGet]   // Dịch vụ GET

        //Lấy thông tin tất cả câu hỏi
        public List<DIEM> GetDiemLists()
        {
            DBGameDataContext db = new DBGameDataContext();
            return db.DIEMs.ToList();
        }

        [HttpPost]
        [Route("api/diem/diemcao")]
        public int GetDiem(DIEM usClient)
        {
            DBGameDataContext db = new DBGameDataContext();
            DIEM diem = db.DIEMs.FirstOrDefault(x => x.IDUser.Equals(usClient.IDUser) && x.IDLoai == usClient.IDLoai);
            if (diem == null) return 0;
            return Int16.Parse(diem.DIEM1.ToString());
        }


        [HttpPost]      //Dịch vụ POST

        // Thêm 1 user mới
        public bool InsertNewDiem(DIEM diemClient)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                DIEM diem = new DIEM();
                //us.ID = id;
                diem.IDUser = diemClient.IDUser;
                diem.LOAICH = diemClient.LOAICH;
                diem.DIEM1 = diemClient.DIEM1;
                db.DIEMs.InsertOnSubmit(diem);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPut]      //Dịch vụ PUT
        public bool UpdateDiem(DIEM diemClient)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                DIEM diem = db.DIEMs.FirstOrDefault(x => x.IDUser == diemClient.IDUser && x.LOAICH == diemClient.LOAICH);

                diem.DIEM1 = diemClient.DIEM1;
                db.DIEMs.InsertOnSubmit(diem);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]    //Dịch vụ DELETE

        //Xóa 1 user
        public bool DeleteDA(DIEM diemClient)
        {
            DBGameDataContext db = new DBGameDataContext();
            //lấy user tồn tại ra
            DIEM diem = db.DIEMs.FirstOrDefault(x => x.IDUser == diemClient.IDUser && x.LOAICH == diemClient.LOAICH);
            if (diem == null) return false;
            db.DIEMs.DeleteOnSubmit(diem);
            db.SubmitChanges();
            return true;
        }
    }
}
