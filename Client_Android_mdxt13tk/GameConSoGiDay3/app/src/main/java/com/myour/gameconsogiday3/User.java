package com.myour.gameconsogiday3;

public class User {
    private String IDUser;
    private String MK;
    private String HoTen;

    public User(String IDUser, String MK, String hoTen) {
        this.IDUser = IDUser;
        this.MK = MK;
        HoTen = hoTen;
    }

    public User() {
    }

    public String getIDUser() {
        return IDUser;
    }

    public void setIDUser(String IDUser) {
        this.IDUser = IDUser;
    }

    public String getMK() {
        return MK;
    }

    public void setMK(String MK) {
        this.MK = MK;
    }

    public String getHoTen() {
        return HoTen;
    }

    public void setHoTen(String hoTen) {
        HoTen = hoTen;
    }
}
