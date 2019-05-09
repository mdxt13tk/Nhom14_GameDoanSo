package com.myour.gameconsogiday3;

public class Question {
    private int IDCH;
    private String NoiDung;
    private String CauA;
    private String CauB;
    private String CauC;
    private String CauD;
    private String IDLoai;

    public Question(int IDCH, String noiDung, String cauA, String cauB, String cauC, String cauD, String IDLoai) {
        this.IDCH = IDCH;
        NoiDung = noiDung;
        CauA = cauA;
        CauB = cauB;
        CauC = cauC;
        CauD = cauD;
        this.IDLoai = IDLoai;
    }

    public Question() {
    }

    public int getIDCH() {
        return IDCH;
    }

    public void setIDCH(int IDCH) {
        this.IDCH = IDCH;
    }

    public String getNoiDung() {
        return NoiDung;
    }

    public void setNoiDung(String noiDung) {
        NoiDung = noiDung;
    }

    public String getCauA() {
        return CauA;
    }

    public void setCauA(String cauA) {
        CauA = cauA;
    }

    public String getCauB() {
        return CauB;
    }

    public void setCauB(String cauB) {
        CauB = cauB;
    }

    public String getCauC() {
        return CauC;
    }

    public void setCauC(String cauC) {
        CauC = cauC;
    }

    public String getCauD() {
        return CauD;
    }

    public void setCauD(String cauD) {
        CauD = cauD;
    }

    public String getIDLoai() {
        return IDLoai;
    }

    public void setIDLoai(String IDLoai) {
        this.IDLoai = IDLoai;
    }
}
