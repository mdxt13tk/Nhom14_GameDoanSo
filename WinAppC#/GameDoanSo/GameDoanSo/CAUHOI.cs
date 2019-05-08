using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDoanSo
{
    class CAUHOI
    {
        [JsonProperty("IDCH")] //Thiết lập thuộc tính khi convert sang json
        private int IDCH;

        [JsonProperty("NoiDung")]
        private string NoiDung;

        [JsonProperty("CauA")]
        private string CauA;

        [JsonProperty("CauB")]
        private string CauB;

        [JsonProperty("CauC")]
        private string CauC;

        [JsonProperty("CauD")]
        private string CauD;

        [JsonProperty("IDLoai")]
        private string IDLoai;

        public CAUHOI()
        {
            this.IDCH = 1;
            this.NoiDung = "";
            this.CauA = "";
            this.CauB = "";
            this.CauC = "";
            this.CauD = "";
            this.IDLoai = "";
        }
        public CAUHOI(CAUHOI CH)
        {
            this.IDCH = CH.IDCH;
            this.NoiDung = CH.NoiDung;
            this.CauA = CH.CauA;
            this.CauB = CH.CauB;
            this.CauC = CH.CauC;
            this.CauD = CH.CauD;
            this.IDLoai = CH.IDLoai;
        }
        public int getIDCH()
        {
            return this.IDCH;
        }
        public string getNoiDung()
        {
            return this.NoiDung;
        }
        public string getCauA()
        {
            return this.CauA;
        }
        public string getCauB()
        {
            return this.CauB;
        }
        public string getCauC()
        {
            return this.CauC;
        }
        public string getCauD()
        {
            return this.CauD;
        }
        public string getIDLoai()
        {
            return this.IDLoai;
        }

    }
}
