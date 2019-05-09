package com.myour.gameconsogiday3;

import android.app.AlertDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.CountDownTimer;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.VideoView;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.bumptech.glide.Glide;
import com.google.gson.Gson;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.Delayed;

public class PlayActivity extends AppCompatActivity {
    private TextView tvHighScore, tvScore, tvCountDown, tvQuestion;
    private ProgressBar pgbCountDown;
    private Button btnA, btnB, btnC, btnD;
    private ImageView imgvMinion;
    //dialogWin1010
    private Button btnBackWin1010,btnExitWin1010;
    private VideoView vvWin1010;
    //dialogLost
    private Button btnBackLost, btnExitLost;
    private VideoView vvLost;
    private TextView tvScoreLost;

    private String level ="";
    private Question question =new Question();
    private int dem=0;
    private CountDownTimer countDownTimer;
    private MediaPlayer mediaPlayer;
    private final String url=Program.url+"/cauhoi/ran";
    private final String id=Program.user.getIDUser();

    private final String urlAnswer = Program.url+"/dapan/kiemtra";
    private final String urlRemove=Program.url+"/cauhoi/remove";

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
        setContentView(R.layout.activity_play);
        
        setControl();

        mediaPlayer=MediaPlayer.create(this,R.raw.play_sound);
        mediaPlayer.start();
        mediaPlayer.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
            @Override
            public void onCompletion(MediaPlayer mp) {
                mp.start();
            }
        });
        Glide.with(this).load(R.raw.minion).into(imgvMinion);
        Intent intent=getIntent();
        level=intent.getStringExtra("#level");
        String highscore=intent.getStringExtra("#highscore");
        tvHighScore.setText("Kỷ lục của bạn:\n"+highscore);
        pgbCountDown.setMax(15);
        sendRequestGetQuestion();

        setEvent();
    }

    private void showDialogLost() {
        mediaPlayer.stop();
        Dialog dialog=new Dialog(this);
        dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        dialog.setContentView(R.layout.dialog_lost);
        dialog.setCanceledOnTouchOutside(false);
        dialog.getWindow().setLayout(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);

        setControlDialogLost(dialog);

        vvLost.setVideoURI(Uri.parse("android.resource://"+getPackageName()+"/"+R.raw.lost2));
        vvLost.setZOrderOnTop(true);
        vvLost.start();
        tvScoreLost.setText((dem-1)*10+" điểm");

        setEventDialogLost(dialog);

        dialog.show();
    }

    private void setEventDialogLost(final Dialog dialog) {
        btnExitLost.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clickBtnExitLost();
            }
        });
        btnBackLost.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dialog.dismiss();
                PlayActivity.this.finish();
            }
        });
        vvLost.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
            @Override
            public void onCompletion(MediaPlayer mp) {
                vvLost.start();
            }
        });
    }

    private void clickBtnExitLost() {
        final AlertDialog.Builder alertDialog=new AlertDialog.Builder(this);
        alertDialog.setTitle("Xác nhận!");
        alertDialog.setIcon(R.mipmap.ic_launcher);
        alertDialog.setMessage("Bạn có thực sự muốn thoát?");
        alertDialog.setPositiveButton("Có", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                finishAffinity();
            }
        });
        alertDialog.setNegativeButton("Không", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
            }
        });
        alertDialog.show();
    }

    private void setControlDialogLost(Dialog dialog) {
        btnBackLost=dialog.findViewById(R.id.buttonBackLost);
        btnExitLost=dialog.findViewById(R.id.buttonExitLost);
        tvScoreLost=dialog.findViewById(R.id.textviewScoreLost);
        vvLost=dialog.findViewById(R.id.videoviewLost);
    }

    private void setEvent() {
        btnA.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                countDownTimer.cancel();
                String idCH= String.valueOf(question.getIDCH());
                sendRequestAnswer(urlAnswer,idCH,"A");
            }
        });
        btnB.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                countDownTimer.cancel();
                String idCH= String.valueOf(question.getIDCH());
                sendRequestAnswer(urlAnswer,idCH,"B");
            }
        });
        btnC.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                countDownTimer.cancel();
                String idCH= String.valueOf(question.getIDCH());
                sendRequestAnswer(urlAnswer,idCH,"C");
            }
        });
        btnD.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                countDownTimer.cancel();
                String idCH= String.valueOf(question.getIDCH());
                sendRequestAnswer(urlAnswer,idCH,"D");
            }
        });
    }

    private void sendRequestAnswer(String urlAnswer, final String idCH, final String answer) {
        StringRequest stringRequest = new StringRequest(Request.Method.POST, urlAnswer, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                if(response.equals("true")){
                    MediaPlayer soundTing=MediaPlayer.create(PlayActivity.this,R.raw.ting2);
                    soundTing.setVolume(100,100);
                    soundTing.start();
                    tvScore.setText("Điểm hiện tại:\n"+dem*10);
                    if (dem<10){
                        sendRequestGetQuestion();
                    } else{
                        Toast.makeText(PlayActivity.this, "CHIẾN THẮNG!", Toast.LENGTH_SHORT).show();
                        String diem=String.valueOf(dem*10);
                        sendRequestRemove(urlRemove,id, level,diem);
                        showDialogWin1010();
                    }
                }else{
                    Toast.makeText(PlayActivity.this, "Bạn đã trả lời sai!", Toast.LENGTH_SHORT).show();
                    String diem=String.valueOf((dem-1)*10);
                    sendRequestRemove(urlRemove,id, level, diem);
                    showDialogLost();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(PlayActivity.this, "Lỗi nè:\n" + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            public Map<String, String> getParams() {
                Map<String, String> jsonRequest = new HashMap<String, String>();
                jsonRequest.put("IDCH", idCH);
                jsonRequest.put("DapAn1",answer);
                return jsonRequest;
            }
        };
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);
    }

    private void showDialogWin1010() {
        mediaPlayer.stop();
        Dialog dialog=new Dialog(this);
        dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        dialog.setContentView(R.layout.dialog_win_1010);
        dialog.setCanceledOnTouchOutside(false);
        dialog.getWindow().setLayout(ViewGroup.LayoutParams.MATCH_PARENT, ViewGroup.LayoutParams.WRAP_CONTENT);

        setControlDialogWin1010(dialog);

        vvWin1010.setVideoURI(Uri.parse("android.resource://"+getPackageName()+"/"+R.raw.win1010));
        vvWin1010.setZOrderOnTop(true);
        vvWin1010.start();

        setEventDialogWin1010(dialog);

        dialog.show();
    }

    private void setEventDialogWin1010(final Dialog dialog) {
        btnBackWin1010.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                dialog.dismiss();
                PlayActivity.this.finish();
            }
        });
        btnExitWin1010.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                clickBtnExitWin1010();
            }
        });
        vvWin1010.setOnCompletionListener(new MediaPlayer.OnCompletionListener() {
            @Override
            public void onCompletion(MediaPlayer mp) {
                vvWin1010.start();
            }
        });
    }

    private void clickBtnExitWin1010() {
        final AlertDialog.Builder alertDialog=new AlertDialog.Builder(this);
        alertDialog.setTitle("Xác nhận!");
        alertDialog.setIcon(R.mipmap.ic_launcher);
        alertDialog.setMessage("Bạn có thực sự muốn thoát?");
        alertDialog.setPositiveButton("Có", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                finishAffinity();
            }
        });
        alertDialog.setNegativeButton("Không", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
            }
        });
        alertDialog.show();
    }

    private void setControlDialogWin1010(Dialog dialog) {
        btnBackWin1010=dialog.findViewById(R.id.buttonBackWin1010);
        btnExitWin1010=dialog.findViewById(R.id.buttonExitWin1010);
        vvWin1010 =dialog.findViewById(R.id.videoviewWin1010);
    }

    private void sendRequestRemove(String urlRemove, final String id, final String level, final String diem) {
        StringRequest stringRequest = new StringRequest(Request.Method.POST, urlRemove, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                int result= Integer.parseInt(response.substring(1,response.length()-1));
                Program.sccore=result;
                Program.level= Integer.parseInt(level);
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(PlayActivity.this, "Lỗi remove:\n" + error, Toast.LENGTH_SHORT).show();
            }
        }) {
            public Map<String, String> getParams() {
                Map<String, String> jsonRequest = new HashMap<String, String>();
                jsonRequest.put("IDUser", id);
                jsonRequest.put("IDLoai",level);
                jsonRequest.put("Diem1",diem);
                return jsonRequest;
            }
        };
        RequestQueue requestQueue = Volley.newRequestQueue(this);
        requestQueue.add(stringRequest);
    }

    private void sendRequestGetQuestion() {
        Map<String, String> json = new HashMap<>();
        json.put("IDUser", id);
        json.put("IDLoai", level);

        JSONObject jsonRequest = new JSONObject(json);
        JsonObjectRequest jsonObjectRequest=new JsonObjectRequest(Request.Method.POST, url, jsonRequest, new Response.Listener<JSONObject>() {
            @Override
            public void onResponse(JSONObject response) {
                dem=dem+1;
                Gson gson=new Gson();
                question =gson.fromJson(String.valueOf(response), Question.class);
                tvQuestion.setText("Câu hỏi "+dem+" : "+ question.getNoiDung());
                btnA.setText("A.    "+ question.getCauA());
                btnB.setText("B.    "+ question.getCauB());
                btnC.setText("C.    "+ question.getCauC());
                btnD.setText("D.    "+ question.getCauD());

                countDownTimer=new CountDownTimer(16000,1000) { //16s thì onTick được 15 lần.
                    int load=1;
                    @Override
                    public void onTick(long millisUntilFinished) {
                        tvCountDown.setText(String.valueOf(16-load));
                        pgbCountDown.setProgress(load);
                        load++;
                    }
                    @Override
                    public void onFinish() {
                        load=1;
                        tvCountDown.setText("END");
                        Toast.makeText(PlayActivity.this, "END", Toast.LENGTH_SHORT).show();
                        String diem=String.valueOf((dem-1)*10);
                        sendRequestRemove(urlRemove,id,level,diem);
                        showDialogLost();
                    }
                };
                countDownTimer.start();
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(PlayActivity.this, "Lỗi:\n"+error, Toast.LENGTH_SHORT).show();
            }
        });
        RequestQueue requestQueue= Volley.newRequestQueue(this);
        requestQueue.add(jsonObjectRequest);
    }

    private void setControl() {
        tvHighScore=findViewById(R.id.textviewHighscore);
        tvScore=findViewById(R.id.textviewScore);
        tvCountDown=findViewById(R.id.textviewCountDown);
        pgbCountDown=findViewById(R.id.progressbarCountDown);
        tvQuestion=findViewById(R.id.textviewQuestion);
        btnA=findViewById(R.id.buttonA);
        btnB=findViewById(R.id.buttonB);
        btnC=findViewById(R.id.buttonC);
        btnD=findViewById(R.id.buttonD);
        imgvMinion=findViewById(R.id.imageviewMinion);
    }

    @Override
    protected void onStop() {
        super.onStop();
        mediaPlayer.stop();
    }
}
