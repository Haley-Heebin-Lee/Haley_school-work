package assignment4.myapplication;

import android.graphics.Bitmap;
import android.os.Handler;
import android.os.Looper;

import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class NetworkingService {
    String newsURL = "https://newsapi.org/v2/everything?q=";
    String newsURL2 = "&from=2021-12-10&sortBy=popularity&apiKey=9d2a9799a6574867ab6a744313e4a8ed";

    public static final ExecutorService networkingExecutor = Executors.newFixedThreadPool(4);
    static Handler networkHandler = new Handler(Looper.getMainLooper());

    interface NetworkingListener{
        void APINetworkListner(String jsonString);
    }
    NetworkingListener listener;
    public void fetchNewsData(String keyword){
        String completeURL = newsURL + keyword + newsURL2;
        connect(completeURL);
    }

    private void connect(String url) {
        networkingExecutor.execute(new Runnable() {
        String jsonString = "";
        @Override
        public void run() {

         HttpURLConnection httpURLConnection = null;
         try {
             URL urlObject = new URL(url);
             httpURLConnection = (HttpURLConnection) urlObject.openConnection();
             httpURLConnection.setRequestMethod("GET");
             httpURLConnection.setRequestProperty("Content-Type", "application/json");
             httpURLConnection.setRequestProperty("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.45 Safari/537.36");
             int statues = httpURLConnection.getResponseCode();

             if ((statues >= 200) && (statues <= 299)) {
                 InputStream in = httpURLConnection.getInputStream();
                 InputStreamReader inputStreamReader = new InputStreamReader(in);
                 int read = 0;
                 while ((read = inputStreamReader.read()) != -1) {// json integers ASCII
                     char c = (char) read;
                     jsonString += c;
                 }// jsonString = {~~}
                 // dataTask in ios
                 final String finalJson = jsonString;
                 networkHandler.post(new Runnable() {
                     @Override
                     public void run() {
                         //send data to main thread
                         listener.APINetworkListner(finalJson);
                     }
                 });
             }
         } catch(MalformedURLException e){
             e.printStackTrace();
         }
         catch(IOException e){
             e.printStackTrace();
         }
         finally{
             httpURLConnection.disconnect();
         }
        }
        });
    }

}
