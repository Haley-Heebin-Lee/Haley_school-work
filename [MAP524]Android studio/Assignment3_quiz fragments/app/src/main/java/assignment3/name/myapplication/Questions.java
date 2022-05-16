package assignment3.name.myapplication;

public class Questions {
    private int answerId;
    private boolean correctAnswer;

    public Questions(int answerResId, boolean answerTrue){
        this.answerId = answerResId;
        this.correctAnswer = answerTrue;
    }
    public Questions(Questions q){
        this.answerId = q.answerId;
        this.correctAnswer = q.correctAnswer;
    }
    public int getAnswerId(){
        return answerId;
    }
    public void setAnswerId(int answerId){
        this.answerId = answerId;
    }
    public boolean correctAnswer(){
        return correctAnswer;
    }
    public void setCorrectAnswer(boolean correctAnswer){
        this.correctAnswer = correctAnswer;
    }
    public boolean isCorrect(boolean inputAnswer){
        if(inputAnswer == this.correctAnswer())
            return true;
        else
            return false;
    }
}
