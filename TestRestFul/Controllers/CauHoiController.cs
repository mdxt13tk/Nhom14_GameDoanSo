using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestRestFul.Controllers
{

    public class CauHoiController : ApiController
    {
        private const int minDe = 1;
        private const int maxDe = 18;
        private const int minVua = 18;
        private const int maxVua = 28;
        private const int minKho = 28;
        private const int maxKho = 38;
        [HttpGet]   // Dịch vụ GET

        //Lấy thông tin tất cả câu hỏi
        public List<CAUHOI> GetCHLists()
        {

            DBGameDataContext db = new DBGameDataContext();
            return db.CAUHOIs.ToList();
        }
        private int rand(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }

        public static Dictionary<string, string> hash = new Dictionary<string, string>();

        [HttpPost]
        [Route("api/cauhoi/ran")]
        //Lấy thông tin theo id
        public CAUHOI GetCH(DIEM diemClient)
        {
            DBGameDataContext db = new DBGameDataContext();
            int id;
            while (true)
            {
                if(diemClient.IDLoai.Trim().Equals("1"))    id = rand(minDe, maxDe);
                else if (diemClient.IDLoai.Trim().Equals("2")) id = rand(minVua, maxVua);
                else id = rand(minKho, maxKho);
                if (hash.Any(x => x.Key == (diemClient.IDUser + id))) continue;
                hash.Add(diemClient.IDUser + id, diemClient.IDUser);
                break;
            }
            CAUHOI ch =  db.CAUHOIs.FirstOrDefault(x => x.IDCH == id && x.IDLoai == diemClient.IDLoai);
            return ch;
        }

        [HttpPost]
        [Route("api/cauhoi/remove")]
        public string RemoveCH(DIEM diemClient)
        {
            DBGameDataContext db = new DBGameDataContext();
            var chrm = hash.Where(x => x.Value == diemClient.IDUser).ToList(); //Get list câu hỏi của User có ID đó.
            foreach (var item in chrm)
            {
                hash.Remove(item.Key);
            }
            DIEM diem = db.DIEMs.FirstOrDefault(x => x.IDUser == diemClient.IDUser && x.IDLoai == diemClient.IDLoai);
            if (diem == null)
            {
                diem = new DIEM();
                diem.IDUser = diemClient.IDUser;
                diem.IDLoai = diemClient.IDLoai;
                diem.DIEM1 = diemClient.DIEM1;
                db.DIEMs.InsertOnSubmit(diem);
                db.SubmitChanges();
            }
            else if (diem.DIEM1 < diemClient.DIEM1)
            {
                diem.DIEM1 = diemClient.DIEM1;
                db.SubmitChanges();
            }
            return diem.DIEM1 + "";
        }

        // Thêm 1 cauhoi mới
        public bool InsertNewCH(string nd, string a, string b, string c, string d, string idLoai)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                CAUHOI ch = new CAUHOI();
                //us.ID = id;
                ch.NoiDung = nd;
                ch.CauA = a;
                ch.CauB = b;
                ch.CauC = c;
                ch.CauD = d;
                ch.IDLoai = idLoai;
                db.CAUHOIs.InsertOnSubmit(ch);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


        [HttpPut]      //Dịch vụ PUT
        public bool UpdateCH(int id, string nd, string a, string b, string c, string d, string idLoai)
        {
            try
            {
                DBGameDataContext db = new DBGameDataContext();
                //lấy user trong database ra
                CAUHOI ch = db.CAUHOIs.FirstOrDefault(x => x.IDCH == id);
                if (ch == null) return false;//không tồn tại false
                ch.NoiDung = nd;
                ch.CauA = a;
                ch.CauB = b;
                ch.CauC = c;
                ch.CauD = d;
                ch.IDLoai = idLoai;
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
        public bool DeleteCH(int id)
        {
            DBGameDataContext db = new DBGameDataContext();
            //lấy user tồn tại ra
            CAUHOI ch = db.CAUHOIs.FirstOrDefault(x => x.IDCH == id);
            if (ch == null) return false;
            db.CAUHOIs.DeleteOnSubmit(ch);
            db.SubmitChanges();
            return true;
        }

    }
}
