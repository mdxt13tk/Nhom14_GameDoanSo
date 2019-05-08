using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDoanSo
{
    class User
    {
        private string IDUser;
        private string MK;
        private string HoTen;

        public User()
        {
            this.IDUser = "";
            this.MK = "";
            this.HoTen = "";

        }
        public User(User user)
        {
            this.IDUser = user.IDUser;
            this.MK = user.MK;
            this.HoTen = user.HoTen;
        }
        public string getIDUser()
        {
            return this.IDUser;
        }
        public string getMK()
        {
            return this.MK;
        }
        public string getHoTen()
        {
            return this.HoTen;
        }
        public void setIDUser(string sID)
        {
            this.IDUser = sID;
        }
        public void setMK(string sMK)
        {
            this.IDUser = sMK;
        }
        public void setHoTen(string sHOTEN)
        {
            this.IDUser = sHOTEN;
        }
    }
}
