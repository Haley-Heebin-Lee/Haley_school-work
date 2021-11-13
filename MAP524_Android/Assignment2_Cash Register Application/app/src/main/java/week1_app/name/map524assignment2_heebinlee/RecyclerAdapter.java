package week1_app.name.map524assignment2_heebinlee;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;

public class RecyclerAdapter extends RecyclerView.Adapter<RecyclerAdapter.viewHolder>{
    ArrayList<History> arrayOfHistory;
    Context mContext;


    public  interface OnItemClickListner{
        void onHistoryClicked(History item);
    }
    private final OnItemClickListner listner;

    public RecyclerAdapter(ArrayList<History> arrayOfHistory, Context context, OnItemClickListner listnerFromActivity)
    {
        this.arrayOfHistory = arrayOfHistory;
        this.mContext = context;
        listner = listnerFromActivity;
    }

    public static class viewHolder extends RecyclerView.ViewHolder{
        private final TextView rProduct;
        private final TextView rQuantity;
        private final TextView rTotal;

        public viewHolder(@NonNull View itemView) {
            super(itemView);
            rProduct = itemView.findViewById(R.id.recyclerProductName);
            rQuantity = itemView.findViewById(R.id.recyclerQuantity);
            rTotal = itemView.findViewById(R.id.recyclerTotalPrice);
        }

        public TextView getPName(){return rProduct;}
        public TextView getPQuantity(){return rQuantity;}
        public TextView getPPrice(){return rTotal;}
    }

    @NonNull
    @Override
    public RecyclerAdapter.viewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater myInflater =  LayoutInflater.from(mContext);
        View view = myInflater.inflate(R.layout.recycler_view_list_row, parent, false);
        return new viewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull viewHolder holder, int position) {
        holder.getPName().setText(arrayOfHistory.get(position).historyProduct);
        holder.getPQuantity().setText(String.valueOf(arrayOfHistory.get(position).historyQuantity));
        holder.getPPrice().setText(String.valueOf(arrayOfHistory.get(position).historyTotal));
        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                listner.onHistoryClicked(arrayOfHistory.get(position));
            }
        });
    }

    @Override
    public int getItemCount() {
        return arrayOfHistory.size();
    }
}
