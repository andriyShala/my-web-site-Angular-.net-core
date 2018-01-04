import {Component} from '@angular/core';
import {Router} from '@angular/router';
import { setInterval } from 'timers';
@Component({
    moduleId:module.id,
    selector:'navlogin',
    templateUrl:'navlogin.component.html',
    styleUrls:['navlogin.component.css']
})
export class NavLoginComponent{
    nameLogin:string="Login";
    constructor(private router:Router)
    {
        setInterval(()=>this.chang(),1000)
    }
    chang()
    {
      if(JSON.parse(localStorage.getItem("currentUser")))
      {
this.nameLogin="Logout";
      }
      else
      {
        this.nameLogin="Login";

      }

    }
    butLoginClick(event)
    {
      
        this.router.navigate(['/login']);
    }
    
}