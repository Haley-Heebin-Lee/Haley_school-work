package week1_app.name.map524assignment2_heebinlee;

import android.os.Parcel;
import android.os.Parcelable;

public class Product implements Parcelable {
    public String productName;
    public int productQuantity;
    public double productPrice;

    Product(String name, int q, double p){
        this.productName = name;
        this.productQuantity = q;
        this.productPrice = p;
    }
    public String getProductName(){
        return this.productName;
    }
    public double getProductPrice(){
        return this.productPrice;
    }
    public int getProductQuantity() {
        return this.productQuantity;
    }

    public void setProductQuantity(int quantity) {
        this.productQuantity = quantity;
    }

    protected Product(Parcel in){
        productQuantity = in.readInt();
        productName = in.readString();
        productPrice = in.readDouble();
    }
    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel dest, int flags) {
        dest.writeString(productName);
        dest.writeInt(productQuantity);
        dest.writeDouble(productPrice);
    }
    public static final Creator<Product> CREATOR = new Creator<Product>() {
        @Override
        public Product createFromParcel(Parcel in) {
            return new Product(in);
        }

        @Override
        public Product[] newArray(int size) {
            return new Product[size];
        }
    };//responsible to determine if it's array of parcel or not

}
