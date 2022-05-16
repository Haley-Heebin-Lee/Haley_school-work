package assignment4.myapplication;

import androidx.room.Dao;
import androidx.room.Delete;
import androidx.room.Insert;
import androidx.room.Query;

import java.util.List;

@Dao
public interface DAONewsDB {

    @Delete
    void deleteNews(News n);

    @Query("SELECT * FROM News")
    List<News> getAllNews();

    @Insert
    void insert(News news);

}
