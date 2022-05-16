package assignment4.myapplication;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class JsonService {
    public ArrayList<News> parseNewsAPIJson(String jsonNews){
       ArrayList<News> newsFromAPI = new ArrayList<>(0);
        try {
            JSONObject jsonObject = new JSONObject(jsonNews);// root
            JSONArray newsArray = jsonObject.getJSONArray("articles");

            JSONObject newsObject;
            String title;
            String author;
            String date;
            String content;


            for(int i = 0; i<newsArray.length(); i++){
                newsObject = newsArray.getJSONObject(i);
                title = newsObject.getString("title");
                author = newsObject.getString("author");
                date = newsObject.getString("publishedAt");
                content = newsObject.getString("content");

                News news = new News(title, author, date, content);
                //news.setId(i+1);
                newsFromAPI.add(news);
            }

        } catch (JSONException e) {
            e.printStackTrace();
        }
        return newsFromAPI;
    }
}
