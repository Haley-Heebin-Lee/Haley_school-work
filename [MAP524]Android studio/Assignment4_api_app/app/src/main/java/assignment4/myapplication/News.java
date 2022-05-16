package assignment4.myapplication;

import android.os.Parcel;
import android.os.Parcelable;

import androidx.room.Entity;
import androidx.room.PrimaryKey;

@Entity
public class News implements Parcelable {

    @PrimaryKey(autoGenerate = true)
    int id;
    String title;
    String author;
    String date;
    String contents;

    public News(){}
    public News(String title, String author, String date, String contents){
        this.title = title;
        this.author = author;
        this.date = date;
        this.contents = contents;
    }

    protected News(Parcel in) {
        title = in.readString();
        author = in.readString();
        date = in.readString();
        contents = in.readString();
    }

    public static final Creator<News> CREATOR = new Creator<News>() {
        @Override
        public News createFromParcel(Parcel in) {
            return new News(in);
        }

        @Override
        public News[] newArray(int size) {
            return new News[size];
        }
    };

    public void setTitle(String title) {
        this.title = title;
    }

    public String getTitle() {
        return title;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public String getAuthor() {
        return author;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getDate() {
        return date;
    }

    public void setContents(String contents) {
        this.contents = contents;
    }

    public String getContents() {
        return contents;
    }

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {

        dest.writeString(title);
        dest.writeString(author);
        dest.writeString(date);
        dest.writeString(contents);
    }
}
