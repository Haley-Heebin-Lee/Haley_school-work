package assignment4.myapplication;

import java.util.ArrayList;

public class NewsManager {

    ArrayList<News> newsList = new ArrayList<>(0);

    public ArrayList<News> getListOfNews(){
        return newsList;
    }

    public void addNews(News news){
        newsList.add(news);
        DatabaseManager.insertNews(news);
    }
    public int newsCount(){return newsList.size();}

    public void deleteNews(News news){
        newsList.remove(news);
        DatabaseManager.deleteNews(news);
    }
}
