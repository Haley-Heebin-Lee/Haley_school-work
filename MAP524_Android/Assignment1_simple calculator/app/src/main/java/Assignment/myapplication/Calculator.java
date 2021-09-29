package Assignment.myapplication;

public class Calculator {
    boolean validateUserInput(String inputText){
        if(inputText.length() > 2 && !inputText.equals(""))
            return true;
        else
            return false;
    }

    int calc(String ch){
        int result=0;

        if(validateUserInput(ch)){
            String [] temp = new String[ch.length()];
            for(int i=0; i<ch.length(); i++){
                temp[i] = ch.charAt(i)+"";
            }
            for(int i = 1; i< temp.length-1; i++){
                if(i==1){
                    if(temp[i].equals("+"))
                        result = Integer.parseInt(temp[i-1])+Integer.parseInt(temp[i+1]);
                    else if(temp[i].equals("-"))
                        result = Integer.parseInt(temp[i-1])-Integer.parseInt(temp[i+1]);
                    else if(temp[i].equals("*"))
                        result = Integer.parseInt(temp[i-1])*Integer.parseInt(temp[i+1]);
                    else if(temp[i].equals("/")){
                        if(temp[i+1].equals("0")){
                            if(temp[i-1].equals("0"))
                                result = 0;
                            else
                                return -9999;
                        }

                        else
                            result = Integer.parseInt(temp[i-1])/Integer.parseInt(temp[i+1]);
                    }

                }
                else{
                    if(temp[i].equals("+"))
                        result = result + Integer.parseInt(temp[i+1]);
                    else if(temp[i].equals("-"))
                        result = result - Integer.parseInt(temp[i+1]);
                    else if(temp[i].equals("*"))
                        result = result * Integer.parseInt(temp[i+1]);
                    else if(temp[i].equals("/")){
                        if(temp[i+1].equals("0")) {
                            if(result == 0)
                                result = 0;
                            else
                                return -9999;
                            //return "Infinity" if I could return string
                            //worked as it's only one digit calculator
                        }
                        else
                            result = result / Integer.parseInt(temp[i+1]);
                    }
                }
            }
        }

        return result;
    }

}
