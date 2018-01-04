import {Component,OnInit} from '@angular/core';
import { Router, ActivatedRoute, Params } from "@angular/router";
import {UserService} from "../../../../_services/index";
import {User, IRom} from "../../../../_models/index";
import {UserMessageService} from "../../../../_services/index";
import { element } from 'protractor';
import { error } from 'util';


@Component({
    moduleId:module.id,
    selector:'mymessage',
    templateUrl:'mymessage.component.html',
    styleUrls:['mymessage.component.css']
})
export class MyMessageComponent implements OnInit{
    messages:any[];
    _postArray:IRom[];
    constructor(private router: Router,
        private activatedRoute: ActivatedRoute,
        private chatService:UserMessageService) {
            chatService.getRomChangeEmitter().subscribe(
                data=>{
                    console.log(data);
                    this.messages.push(data);
                }
            );
    }
    clifff()
    {
        console.log(this.messages);
    }
    ngOnInit()
    {
        this.chatService.getAllDialoges()
        .subscribe
        (
            resultArray=>this.messages=resultArray,
            error=>console.log("Error ::"+error)
        );
        
     
       
    }   
    private message = {
		author: 'tutorialedge',
		message: 'this is a test message'
	}

     sendMsg() {
	
    }
    clickChatPerson(event)
    {
        this.router.navigate([event], { relativeTo: this.activatedRoute });
    }
}