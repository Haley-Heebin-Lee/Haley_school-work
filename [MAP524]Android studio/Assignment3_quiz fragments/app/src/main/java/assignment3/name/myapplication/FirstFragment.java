package assignment3.name.myapplication;

import android.app.Fragment;
import android.content.Context;
import android.content.res.Resources;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.Nullable;


public class FirstFragment extends Fragment {
    TextView Q1;
    private View v;
    Context context;
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        v = inflater.inflate(R.layout.fisrt_fragment,container,false);

        return v;
    }
    /*public void changeText(String newString) {
        Q1 = (TextView)v.findViewById(R.id.Q1);
        Q1.setText(newString);
    }*/

    @Override
    public void onStart() {
        super.onStart();
        Q1 = (TextView) v.findViewById(R.id.Q1);
        Q1.setText(getString(R.string.Q1));
    }
}
