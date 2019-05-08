using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestRestFul.Controllers
{
    public class DapAnController : ApiController
    {
        [HttpGet]   // Dịch vụ GET

        //Lấy thông tin tất cả câu hỏi
        public List<DAPAN> GetDALists()
        {
            DBGameDataContext db = new DBGameDataContext();
            return db.DAPANs.ToList();
        }

        [HttpPost]
        [Route("api/dapan/kiemtra")]
        public bool GetDA(DAPAN dapanFromClient)
        {
            DBGameDataContext db = new DBGameDataContext();
            DAPAN dAPAN  =  db.DAPANs.First(x => x.IDCH == dapanFromClient.IDCH);
            if (dAPAN.DapAn1 == dapanFromClient.DapAn1)
            {
                return true;
            }
            return false;
        }
      


        [HttpPost]      //Dịch vụ POST

        // Thêm 1 user mới
        public bool InsertNewDA(int id, char da)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                DAPAN dapan = new DAPAN();
                //us.ID = id;
                dapan.IDCH = id;
                dapan.DapAn1 = da;
                db.DAPANs.InsertOnSubmit(dapan);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPut]      //Dịch vụ PUT
        public bool UpdateDA(int id, char da)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                //lấy user trong database ra
                DAPAN dapan = db.DAPANs.FirstOrDefault(x => x.IDCH == id);
                if (dapan == null) return false;//không tồn tại false
                dapan.DapAn1 = da;
                db.SubmitChanges();//xác nhận chỉnh sửa
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]    //Dịch vụ DELETE

        //Xóa 1 user
        public bool DeleteDA(int id)
        {
            DBGameDataContext db = new DBGameDataContext();
            //lấy user tồn tại ra
            DAPAN dapan = db.DAPANs.FirstOrDefault(x => x.IDCH == id);
            if (dapan == null) return false;
            db.DAPANs.DeleteOnSubmit(dapan);
            db.SubmitChanges();
            return true;
        }
    }
}
