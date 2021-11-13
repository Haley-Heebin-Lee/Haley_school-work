package week1_app.name.map524assignment2_heebinlee;

import android.os.Bundle;
import android.view.MenuItem;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.appcompat.app.ActionBar;
import androidx.appcompat.app.AppCompatActivity;

import java.util.ArrayList;

public class HistoryDetail extends AppCompatActivity {
    History history;
    TextView detailProductName, detailTotal, detailDate;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.history_details);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);

        if(getIntent().hasExtra("bundle")){
            Bundle bundleFromMainActivity = getIntent().getBundleExtra("bundle");
            history = bundleFromMainActivity.getParcelable("historyItem");
        }
        detailProductName = (TextView) findViewById(R.id.detailProductName);
        detailTotal = (TextView) findViewById(R.id.detailTotalPrice);
        detailDate = (TextView) findViewById(R.id.detailDate);

        detailProductName.setText(history.historyProduct.toString());
        detailTotal.setText(String.valueOf(history.historyTotal));
        detailDate.setText(String.valueOf(history.date));

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
