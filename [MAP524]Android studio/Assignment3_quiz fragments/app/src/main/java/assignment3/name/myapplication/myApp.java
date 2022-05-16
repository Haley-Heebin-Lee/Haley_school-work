package assignment3.name.myapplication;

import android.app.Application;

public class myApp extends Application {
    //this class will be created before any other classes created
    private StorageManager storageManager = new StorageManager();
    public StorageManager getStorageManager() {
        return storageManager;
    }
}
