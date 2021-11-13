package week1_app.name.map524assignment2_heebinlee;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;

import java.util.ArrayList;

public class SecondActivity extends AppCompatActivity {
    ArrayList<History> listOfHistory;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);

        if(getIntent().hasExtra("bundle")){
            Bundle bundleFromMainActivity = getIntent().getBundleExtra("bundle");
            listOfHistory = bundleFromMainActivity.getParcelableArrayList("historyList");
        }

    }
    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                this.finish();
                return true;
        }
        return super.onOptionsItemSelected(item);
    }
    public void historyClicked(View view){

        Intent myIntent = new Intent(this,RecyclerProductList.class);
        Bundle bundle = new Bundle();
        bundle.putParcelableArrayList("historyList",listOfHistory);
        myIntent.putExtra("bundle",bundle);
        startActivity(myIntent);
    }
}