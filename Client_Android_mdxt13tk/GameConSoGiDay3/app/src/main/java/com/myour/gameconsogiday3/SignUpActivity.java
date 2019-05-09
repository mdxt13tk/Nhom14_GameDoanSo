package com.myour.gameconsogiday3;

import android.content.Intent;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import java.util.HashMap;
import java.util.Map;

public class SignUpActivity extends AppCompatActivity {
    private EditText edtName, edtID, edtPW, edtRePw;
    private Button btnDangKi, btnHuy;
    private TextView tvNotify;
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
        setContentView(R.layout.activity_sign_up);
        
        setControl();
        
        setEvent();
    }

    private void setEvent() {
        btnDangKi.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clickBtnDangKi(v);
            }
        });
        btnHuy.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
    }
    private void clickBtnDangKi(View v) {
        String name = edtName.getText().toString().trim();
        String id = edtID.getText().toString().trim();
        String pw = edtPW.getText().toString().trim();
        String rePw = edtRePw.getText().toString().trim();
        if (name.equals("") || id.equals("") || pw.equals("") || rePw.equals("")) {
            Toast.makeText(this, "Thất bại!", Toast.LENGTH_SHORT).show();
            tvNotify.setText("Không được để trống!");
            edtName.requestFocus();
        } else if(name.equals("IdError") || name.equals("PassError")){
            Toast.makeText(this, "Thất bại", Toast.LENGTH_SHORT).show();
            tvNotify.setText("Tên không hợp lệ!");
            edtName.requestFocus();
        } else if(!pw.equals(rePw)){
            Toast.makeText(this, "Thất bại!", Toast.LENGTH_SHORT).show();
            tvNotify.setText("Mật khẩu nhập lại không khớp!");
        } else{
            String url=Program.url+"/user/register";
            sendRequestSignUp(url,name,id,pw);
        }
    }

    private void sendRequestSignUp(String url, final String hoTen, final String id, final String pw) {
        StringRequest stringRequest = new StringRequest(Request.Method.POST, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                if(response.equals("true")){
                    Toast.makeText(SignUpActivity.this, "Thành công!", Toast.LENGTH_SHORT).show();
                    Intent intent=new Intent();
                    intent.putExtra("#id",id);
                    setResult(RESULT_OK,intent);
                    finish();
                }else{
                    Toast.makeText(SignUpActivity.this, "Thất bại", Toast.LENGTH_SHORT).show();
                    tvNotify.setText("ID đã tồn tại!");
                    edtID.requestFocus();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(SignUpActivity.this, "Lỗi:\n" + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            public Map<String, String> getParams() {
                Map<String, String> jsonRequest = new HashMap<String, String>();
                jsonRequest.put("IDUser", id);
                jsonRequest.put("MK", pw);
                jsonRequest.put("HoTen",hoTen);
                return jsonRequest;
            }
        };
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);
    }

    private void setControl() {
        edtName = findViewById(R.id.edittextHoTen);
        edtID = findViewById(R.id.edittextID);
        edtPW = findViewById(R.id.edittextPw);
        edtRePw = findViewById(R.id.edittextRePw);
        btnDangKi = findViewById(R.id.buttonDangKi);
        btnHuy = findViewById(R.id.buttonHuy);
        tvNotify =findViewById(R.id.textviewThongBao);
    }
}
