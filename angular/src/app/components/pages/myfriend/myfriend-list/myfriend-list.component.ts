import {Component,OnInit} from '@angular/core';
import { Router, ActivatedRoute, Params } from "@angular/router";
import { FriendsService, ModalService, UserMessageService } from '../../../../_services/index';



@Component({
    moduleId:module.id,
    selector:'myfriendlist',
    templateUrl:'myfriend-list.component.html',
    styleUrls:["myfriend-list.component.css"]
    
})
export class MyFriendListComponent implements OnInit
{
    usname:string;
    bodyText:string="";
    message:string="";
    friends:any[];
    ngOnInit(): void {
       this.friendsService.getAllFriendsByCurrentUser()
       .subscribe
        (
            resultArray=>this.friends=resultArray,
            error=>console.log("Error ::"+error)
        );
    }
    constructor(private friendsService:FriendsService,private messageService:UserMessageService,private modalService: ModalService)
    {}
    openSendDialog(usname)
    {
         this.usname=usname;
         this.message="";
        this.openModal('custom-modalFriend-SendImage');
    }
    openModal(id: string){
        this.modalService.open(id);
    }

    closeModal(id: string){
        this.modalService.close(id);
    }
    SendMessage()
    {
    this.messageService.sendMessage(this.usname,this.message);
    this.modalService.close('custom-modalFriend-SendImage');
    }
}
