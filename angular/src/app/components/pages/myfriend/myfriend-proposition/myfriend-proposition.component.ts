import {Component,OnInit} from '@angular/core';
import { Router, ActivatedRoute, Params } from "@angular/router";
import { FriendsService } from '../../../../_services/index';
import { DomSanitizer } from '@angular/platform-browser';
import { setTimeout } from 'timers';
import { error } from 'util';



@Component({
    moduleId:module.id,
    selector:'myfriendproposition',
    templateUrl:'myfriend-proposition.component.html',
    styleUrls:["myfriend-proposition.component.css"]
    
})
export class MyFriendPropositionComponent implements OnInit
{
    propositions:any[];
    ngOnInit(): void {
       this.friendsService.getAllProposition()
       .subscribe
        (
            resultArray=>{
                this.propositions=resultArray;
            },
            error=>console.log("Error ::"+error)
        );
        
      
    //    setTimeout(()=>{for(var i=0;i<this.propositions.length;i++)
    //     {
    //         console.log( this.propositions[i].currentImage);
    //         this.propositions[i].currentImage=this.sanitization.bypassSecurityTrustUrl(this.propositions[i].currentImage);
    //         console.log( this.propositions[i].currentImage);
            
    //     }},1000);
    }
    
    constructor(private friendsService:FriendsService,private sanitization: DomSanitizer)
    {}
    answer(id)
    {
        this.friendsService.setPropositionAnswer(id,true)
        
          
    }
    deleteit(id)
    {
        console.log(id);
        this.propositions.forEach( (item, index) => {
            if(item.idproposition === id) this.propositions.splice(index,1);
          });
    }
    gg(id,ss)
    {
        this.friendsService.setPropositionAnswer(id,ss).subscribe(
       
            resultArray=>{
                        this.deleteit(id);
            },
            error=>console.log("Error ::"+error)
       );
        
    }
   
}
