package assignment3.name.myapplication;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Random;
import java.util.stream.IntStream;

public class MainActivity extends AppCompatActivity implements View.OnClickListener{

    Button btnTrue, btnFalse;
    int currentQuestionIndex = 0;
    ProgressBar progressBar;
    Questions[] questionBank = new Questions[]{
            new Questions(R.string.Q1, false),
            new Questions(R.string.Q2, true),
            new Questions(R.string.Q3, true),
            new Questions(R.string.Q4, true)
    };
    //ArrayList<Questions> questions = new ArrayList<Questions>();
    int randomFragmentNum = 0;
    Fragment fragmentObject = null;
    AlertDialog.Builder builder;
    int correct = 0;
    int attempt = 0;
    int[] arrayFromStorage;
    StorageManager storageManager;
    int sumOfCorrect = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        storageManager = ((myApp) getApplication()).getStorageManager();
/*

        questions.add(new Questions(R.string.Q1, false));
        questions.add(new Questions(R.string.Q2, true));
        questions.add(new Questions(R.string.Q3, true));
        questions.add(new Questions(R.string.Q4, true));
*/
        setRandomFragment();
        selectFragment(randomFragmentNum);

        int[] temp = storageManager.getInfoFromInternalPrivateFile(MainActivity.this);
        arrayFromStorage = new int[temp.length];
        arrayFromStorage = temp;

        attempt = arrayFromStorage.length;
        sumOfCorrect = IntStream.of(arrayFromStorage).sum();

        progressBar = (ProgressBar) findViewById(R.id.progressBar);
        btnTrue = (Button) findViewById(R.id.btnTrue);
        btnFalse = (Button) findViewById(R.id.btnFalse);
    }

    @Override
    public void onStart() {
        super.onStart();
        btnTrue.setOnClickListener(this);
        btnFalse.setOnClickListener(this);
    }
    public void setRandomFragment(){
        Random random = new Random();
        randomFragmentNum = random.nextInt(200);
        if (randomFragmentNum < 50) {
            randomFragmentNum = 0;
        } else if (randomFragmentNum < 100) {
            randomFragmentNum = 1;
        }else if (randomFragmentNum < 100) {
            randomFragmentNum = 2;
        }
        else randomFragmentNum = 3;
        /*Collections.shuffle(questions);
        randomFragmentNum = random.nextInt(questions.size());*/
    }
    @Override
    public void onClick(View v) {
        currentQuestionIndex++;

        //Questions q = new Questions(questions.get(randomFragmentNum));

        //true or false, toast
        switch (v.getId()){
            case R.id.btnTrue:
                if(questionBank[randomFragmentNum].isCorrect(true))
                {
                    correct++;
                    Toast.makeText(getApplicationContext(), "Correct", Toast.LENGTH_SHORT).show();
                }
                else
                    Toast.makeText(getApplicationContext(), "False", Toast.LENGTH_SHORT).show();

                break;

            case R.id.btnFalse:
                if(questionBank[randomFragmentNum].isCorrect(false))
                {
                    correct++;
                    Toast.makeText(getApplicationContext(), "Correct", Toast.LENGTH_SHORT).show();
                }
                else
                    Toast.makeText(getApplicationContext(), "True", Toast.LENGTH_SHORT).show();

                break;
        }

        setRandomFragment();
        //next fragment
        if(currentQuestionIndex < 4){
            selectFragment(randomFragmentNum);
        }
        else{
            sumOfCorrect += correct;
        //show builder and set the index to 0
            builder = new AlertDialog.Builder(this);
            builder.create();
            builder.setTitle("Result")
                    .setMessage("Your score is: " + correct + " out of 4");
            builder.setPositiveButton("SAVE", new DialogInterface.OnClickListener() {
                @Override
                public void onClick(DialogInterface dialog, int which) {
                // save the result
                    storageManager.saveInternalPrivateFile(MainActivity.this, correct);
                    finish();
                }
            })
                    .setNegativeButton("IGNORE", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialog, int which) {}});
            builder.show();
            }



        progressBar.setProgress(currentQuestionIndex);
        //Button b = (Button) v;

    }

    public void selectFragment(int index){

        switch (index){
            case 0:
                fragmentObject= new FirstFragment();
                break;
            case 1:
                fragmentObject= new SecondFragment();
                break;
            case 2:
                fragmentObject = new ThirdFragment();
                break;
            case 3:
                fragmentObject = new FourthFragment();
                break;
        }
        FragmentManager fm = getFragmentManager();
        FragmentTransaction transaction = fm.beginTransaction();
        transaction.replace(R.id.frameLayout, fragmentObject);
        transaction.commit();
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.quiz_menu,menu);
        return  true;
    }
    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        super.onOptionsItemSelected(item);

        builder = new AlertDialog.Builder(this);
        builder.create();

        switch (item.getItemId()){
            case R.id.getAvg:{
                builder.setMessage("Your correct answers are " + sumOfCorrect + " in " + attempt +" attempts");
                builder.setPositiveButton("SAVE", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                    }
                })
                        .setNegativeButton("OK", new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog, int which) {}});

                break;
            }
            case R.id.resetResult:{
                storageManager.resetTheStorage(MainActivity.this);
                builder.setMessage("Storage cleared");

            }
        }
        builder.show();
        return true;
    }
}