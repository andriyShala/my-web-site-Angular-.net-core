import {Component,OnInit } from '@angular/core';
import {Router,ActivatedRoute,Params} from '@angular/router';
import{NavComponent} from '../../index';
import './searchperson.component.less';
import { UserService, FriendsService } from '../../../_services/index';
import { DomSanitizer } from '@angular/platform-browser';
import { fail } from 'assert';
@Component({
    moduleId:module.id,
    selector:'SearchPerson',
    templateUrl:'searchperson.component.html'
})
export class SearchPersonComponent implements OnInit{
    loading:boolean;
    searchString:string="";
    ngOnInit(): void {


        this.route.params.forEach((params: Params) => {
            let id =params["id"]; 
        console.log(id);


        });
      this.loading=true;
        this.route.params
        .switchMap((params: Params) => this.service.getAllByString(params['id']))
        .subscribe
        (
            resultArray=>{this.people=resultArray;this.loading=false; },
            error=>{console.log("Error ::"+error);this.loading=false;}
        );
        
        
    }
    showButton(b1,b2,button){
       if(button==="but1")
       {
                if(String(b1).toLowerCase()==="false"&&String(b2).toLowerCase()==="false")
                {
                    return true;
                }
                return false;
        }
         else if(button==="but3")
         {
            if(String(b1).toLowerCase()==="false"&&String(b2).toLowerCase()==="true")
            {
                return true;
            }
            return false;
        }
        else
        {
           
            if(String(b1).toLowerCase()==="true"&&String(b2).toLowerCase()==="false")
            {
                return true;
            }
            else{
                return false;
            }
            
        }
    }

    getBackground(image){
        return  this.sanitization.bypassSecurityTrustStyle("http://localhost:15909/api/Image/?image=1,shala97");
    }
    gg(){
        console.log(this.people);
    }
    people:any[];
 
    constructor(private route: ActivatedRoute,private service:UserService,private sanitization: DomSanitizer,private friendService:FriendsService)
    {
       
        
    }
    addFriend(username,event)
    {
        console.log(event);
        this.friendService.createProposition(username)
        .subscribe(
            ()=>event.target.disabled=true,
            error=>console.log("Error ::"+error)
        );
    }
    search()
    {
        this.people=[];
        console.log(this.searchString);
        this.loading=true;
        
        this.service.getAllByString(this.searchString).subscribe
        (
            resultArray=>{this.people=resultArray;this.loading=false; },
            error=>{console.log("Error ::"+error);this.loading=false;}
        );
        
    }
}