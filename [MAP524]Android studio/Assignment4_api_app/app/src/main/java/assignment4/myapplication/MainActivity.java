package assignment4.myapplication;

import androidx.annotation.NonNull;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.SearchView;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.content.Intent;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;

import java.util.ArrayList;
import java.util.List;

public class MainActivity extends AppCompatActivity implements
        NewsAdapter.newsClickListner,
        NetworkingService.NetworkingListener,
        DatabaseManager.DatabaseListner{

    ArrayList<News> newsList = new ArrayList<News>();
    RecyclerView recyclerView;
    NewsAdapter adapter;
    NetworkingService networkingService;
    JsonService jsonService;
    DatabaseManager dbManager;
    static NewsManager newsManager = new NewsManager();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        recyclerView = findViewById(R.id.newsList);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new NewsAdapter(this, newsList);
        recyclerView.setAdapter(adapter);
        setTitle("Search for news");

        networkingService = ( (MyApp)getApplication()).getNetworkingService();
        jsonService = ( (MyApp)getApplication()).getJsonService();

        networkingService.listener = this;

        dbManager = new DatabaseManager();
        dbManager.getDBInstance(this);
        dbManager.getAllNews();
        dbManager.listner = this;
    }

    public boolean onCreateOptionsMenu(Menu menu) {
        super.onCreateOptionsMenu(menu);
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.search_menu, menu);

        MenuItem searchViewMenuItem = menu.findItem(R.id.search);

        SearchView searchView = (SearchView) searchViewMenuItem.getActionView();
        String searchFor = searchView.getQuery().toString();
        if (!searchFor.isEmpty()) {
            searchView.setIconified(false);
            searchView.setQuery(searchFor, false);
        }

        searchView.setQueryHint("Enter keyword for news");
        searchView.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                Log.d("query", query);
                networkingService.fetchNewsData(query);
                adapter.newsList = new ArrayList<>(0);
                adapter.notifyDataSetChanged();
                return true;
            }
            @Override
            public boolean onQueryTextChange(String newText) {
                Log.d("query change", newText);
                return false;
            }
        });

        return true;
    }
    @Override
    public boolean onOptionsItemSelected(@NonNull MenuItem item) {
        super.onOptionsItemSelected(item);
        switch (item.getItemId()){
            case R.id.newsHistory: {
                Intent toListActivity = new Intent(this, NewsHistoryActivity.class);
                //toListActivity.putParcelableArrayListExtra("listOfNews",newsManager.getListOfNews());
                startActivity(toListActivity);
                break;
            }
            case R.id.exit:{
                break;
            }
        }
        return true;
    }

    @Override
    protected void onResume() {
        super.onResume();
        networkingService = ((MyApp) getApplication()).getNetworkingService();
        jsonService = ((MyApp) getApplication()).getJsonService();
        networkingService.listener = this;
    }

    @Override
    public void newsClicked(News selectedNews) {
        newsManager.addNews(selectedNews);
        Intent intent = new Intent(this,NewsDetailsActivity.class);
        intent.putExtra("news", selectedNews);
        startActivity(intent);

    }

    @Override
    public void APINetworkListner(String jsonString) {
        Log.d("tag", jsonString);// not parsed yet.
        newsList =  jsonService.parseNewsAPIJson(jsonString);
        adapter.newsList = newsList;
        adapter.notifyDataSetChanged();
    }

    @Override
    public void NewsListner(List<News> list) {

    }
}