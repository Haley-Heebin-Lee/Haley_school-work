package week1_app.name.map524assignment2_heebinlee;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity implements View.OnClickListener{

    String pName[] = {};
    int pQuantity[] = {};
    double pPrice[]={};
    ArrayList<Product> arrayOfProduct;
    ArrayList<History> arrayOfHistory;
    ListView productList;
    TextView quantity, productType, total;
    CustomListAdapter adapter;
    AlertDialog.Builder builder;
    Button b0, b1, b2, b3, b4, b5, b6, b7, b8, b9;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        pName = new String[]{"Pants", "Shoes", "Hats"};
        pQuantity = new int[]{10, 100, 30};
        pPrice = new double[]{20.44, 10.44, 5.99};
        if(savedInstanceState == null)
        {//if it's null, set the initial value to the array list
            arrayOfProduct = new ArrayList<Product>(pName.length);
            for(int i=0; i<pName.length; i++){
                Product p = new Product(pName[i], pQuantity[i], pPrice[i]);
                arrayOfProduct.add(p);
            }

            arrayOfHistory = new ArrayList<History>(1);
        }

        else
        {
            arrayOfProduct = savedInstanceState.getParcelableArrayList("listOfProduct");
            arrayOfHistory = savedInstanceState.getParcelableArrayList("listOfHistory");
        }

        //if not null, get the array list

        productList = (ListView) findViewById(R.id.list_view);
        adapter = new CustomListAdapter(this, arrayOfProduct);
        productList.setAdapter(adapter);

        //==========================set the adapter to the list view=================

        quantity = (TextView) findViewById(R.id.quantity);
        productType = (TextView) findViewById(R.id.productType);
        total = (TextView) findViewById(R.id.total);

        productList.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Product obj = (Product) parent.getAdapter().getItem(position);
                productType.setText(obj.getProductName());
                setTotalText();
            }
        });

        //=========================on click listener in list view====================
        findViewButton();
        b0.setOnClickListener(this);
        b1.setOnClickListener(this);
        b2.setOnClickListener(this);
        b3.setOnClickListener(this);
        b4.setOnClickListener(this);
        b5.setOnClickListener(this);
        b6.setOnClickListener(this);
        b7.setOnClickListener(this);
        b8.setOnClickListener(this);
        b9.setOnClickListener(this);
        //================button click=================================================
    }
    @Override
    public void onClick(View v) {
        Button b = (Button) v;
        String str = b.getText().toString();
        setQuantity(str);
    }
    private void setQuantity(String str){
        String q = getString(R.string.quantity);
        //set the quantity text view first
        if(quantity.getText().toString().equals(q))
            quantity.setText(str);
        else
            quantity.setText(quantity.getText().toString()+str);

        setTotalText();
    }

    public void setTotalText() {
        //if the product type & quantity text view is selected
        if(!productType.getText().toString().equals(getString(R.string.productType)) &&
                !productType.getText().toString().equals("") &&
                !quantity.getText().toString().equals(getString(R.string.quantity)) &&
                !quantity.getText().toString().equals(""))
        {
            int position = -1;
            //find the index of the product type in the array pName
            for(int i = 0; i<pName.length; i++){
                if(pName[i].equals(productType.getText().toString())) {
                    position = i;
                    break;
                }
            }
            //get quantity number
            int newQ = Integer.parseInt(quantity.getText().toString());
            //if we found the index of product type
            if(position != -1){
                //get the adapter and price from there
                Product newP = (Product)productList.getAdapter().getItem(position);

                //if the quantity selected are over the stock, warning msg
                if(newQ > newP.getProductQuantity()){
                    Toast.makeText(getApplicationContext(), "No enough quantity in the stock!!!", Toast.LENGTH_SHORT).show();
                }
                //if not, calculate the total and display
                else{
                    Calculate calculate = new Calculate();
                    double result = calculate.totalAmount(newQ, newP.getProductPrice());
                    total.setText(String.format("%.2f", result));
                }
            }

        }

    }

    public void findViewButton(){
        b1 = (Button) findViewById(R.id.button1);
        b2 = (Button) findViewById(R.id.button2);
        b0 = (Button) findViewById(R.id.button0);
        b3 = (Button) findViewById(R.id.button3);
        b4 = (Button) findViewById(R.id.button4);
        b5 = (Button) findViewById(R.id.button5);
        b6 = (Button) findViewById(R.id.button6);
        b7 = (Button) findViewById(R.id.button7);
        b8 = (Button) findViewById(R.id.button8);
        b9 = (Button) findViewById(R.id.button9);
    }
    public void reset(View view){
        quantity.setText("");
        productType.setText("");
    }
    public void buy(View view){
        //if one of the fields(quantity, product type) is empty or initial string
        if(productType.getText().toString().equals(getString(R.string.productType)) || productType.getText().toString().equals("")
            || quantity.getText().toString().equals(getString(R.string.quantity)) || quantity.getText().toString().equals(""))
        {
            Toast.makeText(getApplicationContext(), "All fields are required!!!", Toast.LENGTH_SHORT).show();
        }
        else{
            setNewAdapter();
            //create array of history with the info
            History h = new History(productType.getText().toString(), Integer.parseInt(quantity.getText().toString()), Double.parseDouble(total.getText().toString()));
            arrayOfHistory.add(h);

            //pop up alert after buy
            builder = new AlertDialog.Builder(this);
            builder.create();
            builder.setTitle("Thanks for your purchase");
            builder.setMessage("Your purchase is "+ quantity.getText().toString() +
                    " " + productType.getText().toString()+" for "+ total.getText().toString());
            builder.show();
        }

    }
    public void setNewAdapter(){
        int sold = Integer.parseInt(quantity.getText().toString());
        int[] newQuantity = new int[pName.length];

        //find the index
        int position = -1;
        for(int i = 0; i<pName.length; i++){
            if(pName[i].equals(productType.getText().toString())) {
                position = i;
                break;
            }
        }
        Product newP;
        //found index, copy the original quantity from adapter to the new array of quantity
        if(position != -1) {
            for(int i=0; i<pName.length; i++){
                newP = (Product)productList.getAdapter().getItem(i);
                if(i==position)
                    newQuantity[i] = newP.productQuantity-sold;
                else
                    newQuantity[i] = newP.getProductQuantity();
            }
        }
        //clear array of product and assign new quantity
        arrayOfProduct.clear();
        for(int i=0; i<pName.length; i++){
            Product p = new Product(pName[i], newQuantity[i], pPrice[i]);
            arrayOfProduct.add(p);
        }

        //set the new adapter to the list
        adapter = new CustomListAdapter(this, arrayOfProduct);
        productList.setAdapter(adapter);
    }

    @Override
    protected void onSaveInstanceState(@NonNull Bundle outState) {
        super.onSaveInstanceState(outState);
        /// we need to save the list of product.
        outState.putParcelableArrayList("listOfProduct", arrayOfProduct);
        outState.putParcelableArrayList("listOfHistory", arrayOfHistory);
    }
    public void managerClicked(View view){
        Intent myIntent = new Intent(this, SecondActivity.class);
        Bundle bundle = new Bundle();
        bundle.putParcelableArrayList("historyList", arrayOfHistory);
        myIntent.putExtra("bundle", bundle);
        startActivity(myIntent);
    }
}