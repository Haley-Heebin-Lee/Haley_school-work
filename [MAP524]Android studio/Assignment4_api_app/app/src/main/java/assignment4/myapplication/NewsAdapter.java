package assignment4.myapplication;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

public class NewsAdapter extends RecyclerView.Adapter<NewsAdapter.TasksViewHolder>{
    interface newsClickListner{
        public void newsClicked(News selectedNews);
    }
    private Context context;
    public List<News> newsList;
    newsClickListner listner;

    public NewsAdapter(Context context, List<News> newsList){
        this.context = context;
        this.newsList = newsList;
        listner = (newsClickListner) context;
    }

    @Override
    public TasksViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(context).inflate(R.layout.recyclerview_news, parent, false);
        return new TasksViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull NewsAdapter.TasksViewHolder holder, int position) {
        News news = newsList.get(position);
        holder.getTitleText().setText(news.getTitle());

        String string = news.getDate();
        SimpleDateFormat readingFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss'Z'");
        SimpleDateFormat outputFormat = new SimpleDateFormat("MM-dd");
        try{
            Date parsedDate = readingFormat.parse(string);
            holder.getDateText().setText(outputFormat.format(parsedDate));
        }
        catch(ParseException e){
            e.printStackTrace();
        }
    }

    @Override
    public int getItemCount() {
        return  newsList.size();
    }

    class TasksViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        private final TextView title;
        private final TextView date;

        public TasksViewHolder(View itemView) {
            super(itemView);
            title = itemView.findViewById(R.id.newsTitle);
            date = itemView.findViewById(R.id.newsDate);
            itemView.setOnClickListener(this);
        }
        public TextView getTitleText(){return title;}
        public TextView getDateText(){return date;}
        @Override
        public void onClick(View view) {
            News news = newsList.get(getAdapterPosition());
            listner.newsClicked(news);
        }
    }
}
