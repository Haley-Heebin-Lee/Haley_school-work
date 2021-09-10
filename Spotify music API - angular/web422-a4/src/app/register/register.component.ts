import { Component, OnInit } from '@angular/core';
import { RegisterUser } from '../RegisterUser';
import { AuthService } from '../auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  public registerUser: RegisterUser = {
    userName: "",
    password: "",
    password2: ""
  }

  public warning: string;
  public success:boolean = false;
  public loading:boolean = false;

  constructor(private auth:AuthService) { }

  ngOnInit(): void {
  }

  onSubmit(f: NgForm){
    if(this.registerUser.userName === ""){
      this.warning = 'user name should not be blank';
    }
    if(this.registerUser.password !== this.registerUser.password2){
      this.warning = "passwords must match"
    }
    if(this.registerUser.userName !== "" && this.registerUser.password === this.registerUser.password2){
      this.loading = true;
      this.auth.register(this.registerUser).subscribe(()=>{
        this.success = true;
        this.warning = "";
        this.loading = false;
      }, (err:any)=>{
        this.success = false;
        this.warning =  err.error.message;
        this.loading = false;
      })
    }
    //if putting else if, it will show [object object] in warning message
    
  }
}
