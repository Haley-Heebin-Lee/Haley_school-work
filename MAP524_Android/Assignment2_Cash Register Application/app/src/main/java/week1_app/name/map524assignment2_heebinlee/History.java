package week1_app.name.map524assignment2_heebinlee;

import android.os.Parcel;
import android.os.Parcelable;

import java.util.Calendar;
import java.util.Date;

public class History implements Parcelable{
    public String historyProduct;
    public int historyQuantity;
    public double historyTotal;
    public Date date;

    History(String name, int q, double p){
        this.historyProduct = name;
        this.historyQuantity = q;
        this.historyTotal = p;
        this.date = Calendar.getInstance().getTime();
    }

    protected History(Parcel in){

        historyProduct = in.readString();
        historyQuantity = in.readInt();
        historyTotal = in.readDouble();
        date = new Date(in.readLong());
    }
    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(historyProduct);
        dest.writeInt(historyQuantity);
        dest.writeDouble(historyTotal);
        dest.writeLong(date.getTime());
    }
    public static final Parcelable.Creator<History> CREATOR = new Parcelable.Creator<History>() {
        @Override
        public History createFromParcel(Parcel in) {
            return new History(in);
        }

        @Override
        public History[] newArray(int size) {
            return new History[size];
        }
    };//responsible to determine if it's array of parcel or not

}
