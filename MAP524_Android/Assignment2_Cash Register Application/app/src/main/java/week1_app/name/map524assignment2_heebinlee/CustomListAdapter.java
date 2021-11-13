package week1_app.name.map524assignment2_heebinlee;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;

public class CustomListAdapter extends BaseAdapter {
    private Context context;
    private ArrayList<Product> products;

    public CustomListAdapter(Context context, ArrayList<Product> products){
        this.context = context;
        this.products = products;
    }
    @Override
    public int getCount() {
        return products.size();
        //total product
    }

    @Override
    public Object getItem(int i) {
        return products.get(i);
        //return list product in the index
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int position, View view, ViewGroup parent) {
        View listItemView = view;
        if (listItemView == null) {
            listItemView = LayoutInflater.from(context).inflate(R.layout.list_row_item,null);
        }

        // get the TextView for list
        TextView pName = (TextView) listItemView.findViewById(R.id.product);
        TextView pQuantity = (TextView) listItemView.findViewById(R.id.productQuantity);
        TextView pPrice = (TextView) listItemView.findViewById(R.id.price);
        //sets the text for item name and item description from the current item object

        pName.setText(products.get(position).productName);
        pQuantity.setText(""+products.get(position).productQuantity);
        pPrice.setText(String.valueOf(""+products.get(position).productPrice));

        // returns the view for the current row
        return listItemView;
    }
}
