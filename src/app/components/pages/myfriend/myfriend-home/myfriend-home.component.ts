import { Component,OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { setTimeout } from "timers";

@Component({
    moduleId: module.id,
    selector: "myfriend-home",
    templateUrl: "myfriend-home.component.html",
    styleUrls:['myfriend-home.component.css']
   
})
export class MyFriendHomeComponent implements OnInit{ 
    ngOnInit(): void {
        this.rout();
    }
    activateProp1=false;
    activateProp2=true;
    
    constructor(private route:Router)
    {

    }
    setstyle(str)
    {
        if(str==="proposition")
        {
        this.activateProp1=false;
        this.activateProp2=true;
        
        }
        else{
            this.activateProp1=true;
            this.activateProp2=false;
            
        }
    }
    rout(){
        setTimeout(()=>{this.setstyle(this.route.parseUrl(this.route.url).root.children.primary.segments[1].path)},23);
    }
    gg()
    {
        console.log(this.route.parseUrl(this.route.url));
    }
}