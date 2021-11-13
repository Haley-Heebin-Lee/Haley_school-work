package week1_app.name.map524assignment2_heebinlee;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.MenuItem;
import android.widget.Toast;

import java.util.ArrayList;

public class RecyclerProductList extends AppCompatActivity {
ArrayList<History> arrayOfHistory;
RecyclerView recyclerList;
RecyclerAdapter adapter;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recycler_product_list);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);

        recyclerList = (RecyclerView) findViewById(R.id.recyclerlist);

        if(getIntent().hasExtra("bundle")){
            Bundle bundleFromMainActivity = getIntent().getBundleExtra("bundle");
            arrayOfHistory = bundleFromMainActivity.getParcelableArrayList("historyList");
        }

        Intent myIntent = new Intent(this, HistoryDetail.class);
        Bundle bundle = new Bundle();

        recyclerList.setLayoutManager(new LinearLayoutManager(this));;
        adapter = new RecyclerAdapter(arrayOfHistory, this, new RecyclerAdapter.OnItemClickListner(){
            @Override
            public void onHistoryClicked(History item) {
                //direct to details page!
                bundle.putParcelable("historyItem", item);
                myIntent.putExtra("bundle",bundle);
                startActivity(myIntent);
            }
        });
        recyclerList.setAdapter(adapter);
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
}