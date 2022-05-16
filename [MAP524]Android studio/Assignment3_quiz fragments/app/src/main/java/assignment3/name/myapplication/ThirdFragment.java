package assignment3.name.myapplication;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;


public class ThirdFragment extends Fragment {
    View v;
    TextView Q3;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
       v = inflater.inflate(R.layout.third_fragment,container,false);
        return v;
    }
    @Override
    public void onStart() {
        super.onStart();
        Q3 = (TextView) v.findViewById(R.id.Q3);
        Q3.setText(getString(R.string.Q3));
    }
}
