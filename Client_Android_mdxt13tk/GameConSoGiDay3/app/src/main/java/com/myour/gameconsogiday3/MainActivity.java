package com.myour.gameconsogiday3;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class MainActivity extends AppCompatActivity {
    private EditText edtID, edtPw;
    private Button btnSignUp, btnSignIn, btnExit;
    private TextView tvNotify;
    private CheckBox chbRemember;
    public static final int SIGNUP_REQUEST_CODE=1;
    public SharedPreferences sharedPreferences;
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
        setContentView(R.layout.activity_main);
        
        setControl();

        sharedPreferences=getSharedPreferences("userLogin", MODE_PRIVATE);
        edtID.setText(sharedPreferences.getString("id",""));
        edtPw.setText(sharedPreferences.getString("pw",""));
        chbRemember.setChecked(sharedPreferences.getBoolean("remember",false));

        setEvent();
    }

    private void sendRequestDangNhap(String url, final String id, final String pw) {
        StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                String result=response.substring(1,response.length()-1);
                if(result.equals("IdError")){
                    Toast.makeText(MainActivity.this, "Thất bại!", Toast.LENGTH_SHORT).show();
                    tvNotify.setText("Tài khoản "+id+" không tồn tại!");
                    edtID.requestFocus();
                } else if(result.equals("PassError")){
                    Toast.makeText(MainActivity.this, "Thất bại!", Toast.LENGTH_SHORT).show();
                    tvNotify.setText("Sai mật khẩu, vui lòng nhập lại mật khẩu!");
                    edtPw.requestFocus();
                } else {
                    Toast.makeText(MainActivity.this, "Thành công!", Toast.LENGTH_SHORT).show();
                    Program.user=new User();
                    Program.user.setIDUser(id);
                    Program.user.setMK(pw);
                    Program.user.setHoTen(result);
                    tvNotify.setText("");
                    if (chbRemember.isChecked()){
                        SharedPreferences.Editor editor=sharedPreferences.edit();
                        editor.putString("id",id);
                        editor.putString("pw",pw);
                        editor.putBoolean("remember",true);
                        editor.commit();
                    } else {
                        SharedPreferences.Editor editor=sharedPreferences.edit();
                        editor.remove("id");
                        editor.remove("pw");
                        editor.remove("remember");
                        editor.commit();
                    }
                    Intent intent=new Intent(MainActivity.this, StartActivity.class);
                    startActivity(intent);
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(MainActivity.this, "Lỗi:\n" + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            public Map<String, String> getParams() {
                Map<String, String> jsonRequest = new HashMap<String, String>();
                jsonRequest.put("IDUser", id);
                jsonRequest.put("MK", pw);
                return jsonRequest;
            }
        };
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);

    }
    private void setEvent() {
        btnSignIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clickBtnDangNhap(v);
            }
        });
        btnSignUp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clickBtnDangKi(v);
            }
        });
        btnExit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
    }
    private void clickBtnDangNhap(View v) {
        String id = edtID.getText().toString().trim();
        String pw = edtPw.getText().toString().trim();
        if(id.equals("") || pw.equals("")) {
            Toast.makeText(this, "Thất bại!", Toast.LENGTH_SHORT).show();
            tvNotify.setText("Không được để trống!");
            edtID.requestFocus();
//        }else if(pw.equals("")){
//            Toast.makeText(this, "Thất bại!", Toast.LENGTH_SHORT).show();
//            tvNotify.setText("Không được để trống!");
//            edtPw.requestFocus();
        }else {
            String url = Program.url + "/user/login";
            sendRequestDangNhap(url,id,pw);
        }
    }

    private void clickBtnDangKi(View v) {
        Intent intent = new Intent(this, SignUpActivity.class);
        startActivityForResult(intent,SIGNUP_REQUEST_CODE);
    }
    private void setControl() {
        edtID = findViewById(R.id.edittextID);
        edtPw = findViewById(R.id.edittextPw);
        btnSignUp = findViewById(R.id.buttonDangKi);
        btnSignIn = findViewById(R.id.buttonDangNhap);
        btnExit = findViewById(R.id.buttonThoat);
        tvNotify =findViewById(R.id.textviewThongBao);
        chbRemember=findViewById(R.id.chbRememberSignIn);
    }
    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        if(requestCode==SIGNUP_REQUEST_CODE){
            if(resultCode==RESULT_OK){
                edtID.setText(data.getStringExtra("#id"));
                edtPw.requestFocus();
            }
        }
    }
}
