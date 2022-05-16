package assignment4.myapplication;

import android.content.Context;
import android.os.Handler;
import android.os.Looper;

import androidx.room.Room;

import java.util.List;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class DatabaseManager {

    public interface DatabaseListner{
        void NewsListner(List<News> list);
    }

    static DatabaseListner listner;
    public static final ExecutorService databaseExecuter = Executors.newFixedThreadPool(4);
    static Handler db_handler = new Handler(Looper.getMainLooper());

    static NewsDatabase db;

    public static void buildDBInstance(Context context){
        db = Room.databaseBuilder(context,
                NewsDatabase.class, "database-news").build();
    }
    public static NewsDatabase getDBInstance(Context context){
        if (db == null){
            buildDBInstance(context);
        }
        return db;
    }

    public static void getAllNews() {
        databaseExecuter.execute(new Runnable() {
            @Override
            public void run() {
                List<News> list = db.getDao().getAllNews();
                db_handler.post(new Runnable() {
                    @Override
                    public void run() {
                        // run in main thead
                        listner.NewsListner(list);
                    }
                });
            }
        });
    }
    public static void insertNews(News news){
        // backgound thread
        databaseExecuter.execute(new Runnable() {
            @Override
            public void run() {
                db.getDao().insert(news);
            }
        });
    }
    public static void deleteNews(News news){
        databaseExecuter.execute(new Runnable() {
            @Override
            public void run() {
                db.getDao().deleteNews(news);
            }
        });
    }


}
