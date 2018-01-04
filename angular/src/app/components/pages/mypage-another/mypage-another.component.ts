import {Component,OnInit} from '@angular/core';

import {UserService} from "../../../_services/index";
import {User} from "../../../_models/index";
import {AppConfig} from "../.../../../../app.config"
import { ModalService } from '../../../_services/index';

@Component({
    moduleId:module.id,
    selector:'mypage',
    templateUrl:'mypage.component.html',
    styleUrls:['mypage.component.css']
})
export class MyPageComponent implements OnInit{
    private bodyText: string;
    currentUser: User;
    srcimage:string;
    
    constructor(private userService: UserService,private config:AppConfig, private modalService: ModalService)
    {
        this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
     this.currentUser.firstName   
        
    }
    ngOnInit() {
        this.srcimage=this.config.apiUrl+"/api/Image/?image="+this.currentUser.currentImage+","+this.currentUser.username;
        
      }
      
      updateName() {
        console.log("updateName");
        this.srcimage=this.config.apiUrl+"/api/Image/?image="+this.currentUser.currentImage+","+this.currentUser.username;
      }
      
      openModal(id: string){
        this.modalService.open(id);
    }

    closeModal(id: string){
        this.modalService.close(id);
    }
    
    
}