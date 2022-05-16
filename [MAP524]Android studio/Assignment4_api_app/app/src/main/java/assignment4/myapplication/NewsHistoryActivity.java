package assignment4.myapplication;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.widget.ListView;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

public class NewsHistoryActivity extends AppCompatActivity implements
        DatabaseManager.DatabaseListner,
        NewsAdapter.newsClickListner{

    ArrayList<News> newsList = new ArrayList<News>(0);
    TextView numOfNews;
    RecyclerView recyclerView;

    static NewsManager newsManager = new NewsManager();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_news_history);

        numOfNews = findViewById(R.id.numOfNews);
        recyclerView = findViewById(R.id.newsHistory);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));

        //newsList = getIntent().getParcelableArrayListExtra("listOfNews");

        DatabaseManager.listner = this;
        DatabaseManager.getAllNews();

    }

    @Override
    public void NewsListner(List<News> list) {
        newsList = (ArrayList<News>) list;
        newsManager.newsList = newsList;

        NewsAdapter adapter = new NewsAdapter(this, list);
        adapter.notifyDataSetChanged();
        recyclerView.setAdapter(adapter);
        numOfNews.setText("The number of selected news is " + list.size());
    }

    @Override
    public void newsClicked(News selectedNews) {
        newsManager.deleteNews(selectedNews);
        newsList = newsManager.getListOfNews();
        NewsAdapter adapter = new NewsAdapter(this, newsList);
        adapter.notifyDataSetChanged();
        recyclerView.setAdapter(adapter);
        numOfNews.setText("The number of selected news is " + newsList.size());
    }
}