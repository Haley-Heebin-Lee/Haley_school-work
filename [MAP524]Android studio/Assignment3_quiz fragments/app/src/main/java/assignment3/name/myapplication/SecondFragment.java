package assignment3.name.myapplication;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

public class SecondFragment extends Fragment {
    View v;
    TextView Q2;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        v = inflater.inflate(R.layout.second_fragment,container,false);

        return v;
    }
    @Override
    public void onStart() {
        super.onStart();
        Q2 = (TextView) v.findViewById(R.id.Q2);
        Q2.setText(getString(R.string.Q2));
    }
}
