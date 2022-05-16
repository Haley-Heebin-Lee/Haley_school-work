package assignment3.name.myapplication;

import android.app.Activity;
import android.content.Context;

import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.Inet4Address;
import java.util.ArrayList;

public class StorageManager {
    String filename = "correct.txt";


    public void resetTheStorage(Activity activity){
        FileOutputStream fileOutputStream = null;
        try {
            fileOutputStream = activity.openFileOutput(filename, Context.MODE_PRIVATE); // reset
            fileOutputStream.write("".getBytes());

        }catch (Exception ex) {
            ex.printStackTrace();
        }finally {
            try {
                fileOutputStream.close();
            }catch (IOException ex){
                ex.printStackTrace();
            }
        }
    }

    // 90
    public void saveInternalPrivateFile(Activity activity, int numberOfCorrect){
        FileOutputStream fileOutputStream = null;
        try {
            fileOutputStream = activity.openFileOutput(filename, Context.MODE_APPEND); // continue writting
            fileOutputStream.write((numberOfCorrect+"$").getBytes());

        }catch (Exception ex) {
            ex.printStackTrace();
        }finally {
            try {
                fileOutputStream.close();
            }catch (IOException ex){
                ex.printStackTrace();
            }
        }
        // internal Stream
    }

    public int[] getInfoFromInternalPrivateFile(Activity activity)  {
        FileInputStream fileInputStream = null;
        int read;
        int[] list = null;
        StringBuffer buffer = new StringBuffer();
        try {
            fileInputStream = activity.openFileInput(filename);
            while(( read = fileInputStream.read() )!= -1 ){
                buffer.append((char)read);
            }
            list = new int[(buffer.toString().toCharArray().length)/2];
            list = fromStringToInt(buffer.toString(), list.length);
        }catch (IOException ex){ex.printStackTrace();}
        finally {
            try {
                fileInputStream.close();
            }catch (IOException ex){
                ex.printStackTrace();
            }

        }
        return list;
    }


    private int[] fromStringToInt(String str, int size){ // str come from the file
        // there is a $ between numbers
        int[] list = new int[size];
        int index = 0;
        if(str!=null && !str.equals(""))
        {
          int listIndex = 0;
                for (int i = 0 ; i < str.toCharArray().length ; i++){
                    if (str.toCharArray()[i] == '$'){
                        String numOfCorrect = str.substring(index, i);
                        list[listIndex] = Integer.parseInt(numOfCorrect);
                        index = i+1;
                        listIndex++;
                    }
                }

        }



        return list;

    }
}
