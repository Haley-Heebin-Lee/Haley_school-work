package assignment3.name.myapplication;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;


public class FourthFragment extends Fragment {
    View v;
    TextView Q4;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        v =  inflater.inflate(R.layout.fourth_fragment,container,false);
        return v;
    }
    @Override
    public void onStart() {
        super.onStart();
        Q4 = (TextView) v.findViewById(R.id.Q4);
        Q4.setText(getString(R.string.Q4));
    }
}
