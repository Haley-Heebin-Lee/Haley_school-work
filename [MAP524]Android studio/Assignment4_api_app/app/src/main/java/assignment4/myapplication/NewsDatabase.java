package assignment4.myapplication;

import androidx.room.Database;
import androidx.room.RoomDatabase;

@Database(version = 1, entities = {News.class})
public abstract class NewsDatabase extends RoomDatabase {
    public abstract DAONewsDB getDao();
}
