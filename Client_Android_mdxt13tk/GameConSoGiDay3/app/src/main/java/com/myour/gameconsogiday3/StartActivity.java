package com.myour.gameconsogiday3;

import android.app.Dialog;
import android.content.Intent;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;

import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class StartActivity extends AppCompatActivity {
    private TextView tvID, tvHighScore, tvName;
    private Button btnPlay, btnSignOut, btnInfo;
    private Spinner spnLevel;
    //dialogEditProfile
    private EditText edtName, edtID, edtPw, edtRePw;
    private Button btnEdit, btnCancel;
    private TextView tvNotify;

    private boolean edit = false;
    private ArrayList<String> levelList = new ArrayList<>();
    private int level=-1;
    private String highscore="";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //hide action bar
        ActionBar actionBar = getSupportActionBar();
        actionBar.hide();

        //hide status bar and navigation bar
        getWindow().getDecorView().setSystemUiVisibility(
                View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
                        | View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY
                        | View.SYSTEM_UI_FLAG_FULLSCREEN);
        setContentView(R.layout.activity_start);

        setControl();

        levelList.add("Dễ");
        levelList.add("Vừa");
        levelList.add("Khó");
        ArrayAdapter levelAdapter = new ArrayAdapter(this, R.layout.support_simple_spinner_dropdown_item, levelList);
        spnLevel.setAdapter(levelAdapter);

        tvID.setText("ID: " + Program.user.getIDUser());
        tvName.setText("Xin chào bạn: " + Program.user.getHoTen());

        setEvent();
    }

    private void setEvent() {
        spnLevel.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                level = position + 1;
                String url = Program.url + "/diem/diemcao";
                String idUser = Program.user.getIDUser();
                String levelStr = String.valueOf(level);
                sendRequestGetHighScore(url, idUser, levelStr);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                //do nothing
            }
        });
        btnSignOut.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String url = Program.url + "/user/logout";
                String id = Program.user.getIDUser();
                sendRequestSignOut(url, id);
            }
        });
        btnInfo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                showDialogEditProfile();
            }
        });
        btnPlay.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clickBtnPlay();
            }
        });
    }

    private void clickBtnPlay() {
        Intent intent=new Intent(this,PlayActivity.class);
        intent.putExtra("#level",String.valueOf(level));
        intent.putExtra("#highscore",String.valueOf(highscore));
        startActivity(intent);
    }

    private void sendRequestGetHighScore(String url, final String id, final String levelStr) {
        StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                highscore=response;
                tvHighScore.setText("Kỷ lục: "+response+" - "+levelList.get(Integer.parseInt(levelStr)-1));
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(StartActivity.this, "Lỗi:\n" + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            public Map<String, String> getParams() {
                Map<String, String> jsonRequest = new HashMap<String, String>();
                jsonRequest.put("IDUser", id);
                jsonRequest.put("IDLoai", levelStr);
                return jsonRequest;
            }
        };
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);
    }

    private void showDialogEditProfile() {
        Dialog dialog = new Dialog(this);
        dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        dialog.setContentView(R.layout.dialog_edit_profile);
        dialog.setCanceledOnTouchOutside(false);
        dialog.getWindow().setLayout(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);

        setControlDialogEditProfile(dialog);

        edtID.setText(Program.user.getIDUser());
        edtName.setText(Program.user.getHoTen());
        edtPw.setText(Program.user.getMK());
        edtRePw.setText(edtPw.getText());
        edtID.setEnabled(false);
        edtName.setEnabled(false);
        edtPw.setEnabled(false);
        edtRePw.setVisibility(View.INVISIBLE);

        setEventDialogEditProfile(dialog);

        dialog.show();
    }

    private void setEventDialogEditProfile(final Dialog dialog) {
        btnCancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dialog.dismiss();
                edit = false;
            }
        });
        btnEdit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) { //nhấn sửa
                if (edit == false) {
                    //chế độ sửa
                    edtName.setEnabled(true);
                    edtPw.setEnabled(true);
                    edtRePw.setVisibility(View.VISIBLE);
                    btnEdit.setText("Lưu");
                    edit = true;
                } else if (edit == true) { //nhấn lưu
                    //chế độ lưu
                    String id = Program.user.getIDUser();
                    String name = edtName.getText().toString().trim();
                    String pw = edtPw.getText().toString().trim();
                    String rePw = edtRePw.getText().toString().trim();
                    if (name.equals("") || pw.equals("") || rePw.equals("")) {
                        Toast.makeText(StartActivity.this, "Thất bại!", Toast.LENGTH_SHORT).show();
                        tvNotify.setText("Không được để trống!");
                        edtName.requestFocus();
                    } else if (name.equals("IdError") || name.equals("PassError")) {
                        Toast.makeText(StartActivity.this, "Thất bại", Toast.LENGTH_SHORT).show();
                        tvNotify.setText("Tên không hợp lệ!");
                        edtName.requestFocus();
                    } else if (!pw.equals(rePw)) {
                        Toast.makeText(StartActivity.this, "Thất bại!", Toast.LENGTH_SHORT).show();
                        tvNotify.setText("Mật khẩu nhập lại không khớp!");
                    } else {
                        String url = Program.url + "/user/update";
                        sendRequestEditProfile(url, name, id, pw, dialog);
                    }
                }
            }
        });
    }

    private void sendRequestEditProfile(String url, final String name, final String id, final String pw, final Dialog dialog) {
        StringRequest stringRequest = new StringRequest(Request.Method.PUT, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                if (response.equals("true")) {
                    Toast.makeText(StartActivity.this, "Thành công!", Toast.LENGTH_SHORT).show();
                    Program.user.setHoTen(name);
                    Program.user.setMK(pw);
                    tvNotify.setText("");
                    edtName.setEnabled(false);
                    edtPw.setEnabled(false);
                    edtRePw.setVisibility(View.INVISIBLE);
                    btnEdit.setText("Sửa");
                    edit = false;
                    tvName.setText(Program.user.getHoTen());
                    dialog.dismiss();
                } else {
                    Toast.makeText(StartActivity.this, "Xảy ra lỗi!", Toast.LENGTH_SHORT).show();
                    dialog.dismiss();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(StartActivity.this, "Lỗi:\n" + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            public Map<String, String> getParams() {
                Map<String, String> jsonRequest = new HashMap<String, String>();
                jsonRequest.put("IDUser", id);
                jsonRequest.put("MK", pw);
                jsonRequest.put("HoTen", name);
                return jsonRequest;
            }
        };
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);
    }

    private void setControlDialogEditProfile(Dialog dialog) {
        edtName = dialog.findViewById(R.id.edittextName);
        edtID = dialog.findViewById(R.id.edittextID);
        edtPw = dialog.findViewById(R.id.edittextPw);
        edtRePw = dialog.findViewById(R.id.edittextRePw);
        btnEdit = dialog.findViewById(R.id.buttonEdit);
        btnCancel = dialog.findViewById(R.id.buttonCancel);
        tvNotify = dialog.findViewById(R.id.textviewNotify);
    }

    private void sendRequestSignOut(String url, final String id) {
        StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                if (response.equals("true")) {
                    Program.user = null;
                    finish();
                } else {
                    Toast.makeText(StartActivity.this, "Xảy ra lỗi!", Toast.LENGTH_SHORT).show();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(StartActivity.this, "Lỗi:\n" + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            public Map<String, String> getParams() {
                Map<String, String> jsonRequest = new HashMap<String, String>();
                jsonRequest.put("IDUser", id);
                return jsonRequest;
            }
        };
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);
    }

    private void setControl() {
        tvID = findViewById(R.id.textviewID);
        tvHighScore = findViewById(R.id.textviewHighscore);
        tvName = findViewById(R.id.textviewName);
        btnPlay = findViewById(R.id.buttonPlay);
        btnSignOut = findViewById(R.id.buttonSignOut);
        btnInfo = findViewById(R.id.buttonInfo);
        spnLevel = findViewById(R.id.spinnerLevel);
    }

    @Override
    protected void onStart() {
        super.onStart();
        if(Program.sccore!=-1 && Program.level!=-1){
            tvHighScore.setText("Kỷ lục: "+Program.sccore+" - "+levelList.get(Program.level-1));
        }
    }
}
