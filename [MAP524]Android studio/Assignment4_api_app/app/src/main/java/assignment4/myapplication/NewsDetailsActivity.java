package assignment4.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.TextView;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class NewsDetailsActivity extends AppCompatActivity {

    TextView title, date, author, content;
   /* NetworkingService networkingService;
    JsonService jsonService;*/

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_news_details);
       /* networkingService = ( (MyApp)getApplication()).getNetworkingService();
        jsonService = ( (MyApp)getApplication()).getJsonService();
        networkingService.listener = this;*/

        /*String newsTitle = getIntent().getStringExtra("title");
        String newsAuthor = getIntent().getStringExtra("author");
        String newsDate = getIntent().getStringExtra("date");
        String newsContent = getIntent().getStringExtra("contents");*/

        News news = getIntent().getParcelableExtra("news");


        title = findViewById(R.id.title);
        author = findViewById(R.id.author);
        content = findViewById(R.id.content);
        date = findViewById(R.id.date);

        title.setText(news.getTitle());
        author.setText(news.getAuthor());
        content.setText(news.getContents());

        SimpleDateFormat readingFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'");
        SimpleDateFormat outputFormat = new SimpleDateFormat("MM-dd");
        try{
            Date parsedDate = readingFormat.parse(news.getDate());
            date.setText(outputFormat.format(parsedDate));
        }
        catch(ParseException e){
            e.printStackTrace();
        }
    }

    /*@Override
    public void APINetworkListner(String jsonString) {
        WeatherData weatherData = jsonService.parseWeatherAPIData(jsonString);
        weatherText.setText(weatherData.description + " : "+weatherData.temp );
        networkingService.getImageData(weatherData.icon);
    }*/
}