package Assignment.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;

public class MainActivity extends AppCompatActivity implements View.OnClickListener{

    String tag = "Assignment 1";
    Button advanced;
    TextView calculation;
    TextView historyText;
    TextView warningMsg;
    boolean isAdvance = false;


    private Calculator calculator;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.d(tag, "In OnCreate");
        setContentView(R.layout.activity_main);
        calculator = new Calculator();

        advanced = (Button)findViewById(R.id.advanced);

        calculation = (TextView)findViewById(R.id.calculation);
        historyText = (TextView)findViewById(R.id.historyText);
        warningMsg = (TextView) findViewById(R.id.warningMsg);

    }
    private void calText(String str){
        int numLen = calculation.getText().length();
        String lastChar = null;
        if(numLen>0)
        {
            lastChar = calculation.getText().charAt(numLen - 1) + "";
        }

        if(!str.equals("=")){
            if(getString(R.string.calculation).equals(calculation.getText().toString()) && !isOp(str))
            //first input
            {
                calculation.setText(str);
                warningMsg.setText("");
            }
            else if(lastChar!=null && isOp(lastChar) && isOp(str)){
                //if the last character and new character are op or number, print toast
                //it allows valid input
                warningMsg.setText("Invalid Input");
            }
            else if(lastChar!=null && !isOp(lastChar) && !isOp(str)){
                //it allows only one digit of number
                warningMsg.setText("Only one digit allowed");
            }
            else{
                calculation.setText(calculation.getText().toString()+str);
                warningMsg.setText("");
            }

        }
        else{
            int temp = calculator.calc(calculation.getText().toString());
            if(temp==-9999)
            {
                calculation.setText("");
                warningMsg.setText("Cannot divide by 0");
            }
            else
                calculation.setText(calculation.getText().toString()+"="+temp);
        }

    }
    public boolean isOp(String str){
        boolean val = false;
        if(str.equals("+") || str.equals("-") || str.equals("*") || str.equals("/"))
            val=true;
        //if the string is operand, return true
        return val;
    }

    public void Button0(View view){
        calText("0");
    }
    public void Button1(View view){
        calText("1");
    }
    public void Button2(View view){
        calText("2");
    }
    public void Button3(View view){
        calText("3");
    }
    public void Button4(View view){
        calText("4");
    }
    public void Button5(View view){
        calText("5");
    }
    public void Button6(View view){
        calText("6");
    }
    public void Button7(View view){
        calText("7");
    }
    public void Button8(View view){
        calText("8");
    }
    public void Button9(View view){
        calText("9");
    }
    public void PlusOp(View view){
        calText("+");
    }
    public void MinusOp(View view){
        calText("-");
    }
    public void MultiOp(View view){
        calText("*");
    }
    public void DividOp(View view){
        calText("/");
    }
    public void Reset(View view){
        calculation.setText("");
    }
    public void EqualOp(View view){
        calText("=");
        if(isAdvance)
            historyText.setText(historyText.getText().toString()+"\n"+calculation.getText().toString());
        else
            historyText.setText("");
    }
    public void AdvancedMode(View view){
        if(isAdvance){
            advanced.setText("STANDARD - NO HISTORY");
            historyText.setText("");
            isAdvance = false;
        }
        else{
            advanced.setText("ADVANCED - WITH HISTORY");
            isAdvance = true;
        }
    }
    @Override
    public void onClick(View view) {

    }
}