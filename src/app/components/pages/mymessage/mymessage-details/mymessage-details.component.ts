import { Router, ActivatedRoute, Params } from "@angular/router";
import {Component,OnInit,Input,Output,EventEmitter,ViewChild,ElementRef } from '@angular/core';
import {UserMessageService} from "../../../../_services/index";
import './mymessage-details.component.less';
@Component({
    moduleId: module.id,
    selector: "mymessage-details",
    templateUrl: "mymessage-details.component.html"
})
export class MymessageDetailsComponent implements OnInit {
    @ViewChild('scrollMe') private myScrollContainer: ElementRef;
    message:string="";
    messages:any[];
    recipient:string;
    id:number;

    constructor(private router: Router,
        private activatedRoute: ActivatedRoute,private chatService:UserMessageService) {
          
          chatService.getMessageChangeEmitter().subscribe(
              data=>{ if(data.message!="firstCon")
              {
                  this.messages.push({"text":data.message,"self":false});
                  setTimeout(() => {
                      this.ScrollToBottom();
                    },100);
              }});            
        }
        sendMessage()
        {
            this.messages.push({"text":this.message,"self":true});
            this.chatService.sendMessage(this.recipient,this.message);
            this.message="";
            this.ScrollToBottom();
            setTimeout(() => {
                this.ScrollToBottom();
              },100);
        }

        
        ngOnInit() {
            // params - параметры текущего маршрута. Данное свойство является Observable объектом
            // Если параметры будут изменены - произойдет событие и компонент узнает о изменениях.
    
            // forEach - устанавливаем обработчик на каждое изменение params
            this.activatedRoute.params.forEach((params: Params) => {
                let id = +params["id"]; // конвертируем значение параметра id в тип number
                this.id=params["id"];
                this.chatService
                .getMessagesByRomId(id).subscribe
                (
                    resultArray=>this.messages=resultArray,
                    error=>console.log("Error ::"+error)
                );
                    
            });
            this.chatService.getRecipientByRomId(this.id)
            .subscribe
            (
                resultArray=>{this.recipient=resultArray.username},
                error=>console.log("Error ::"+error)
            );
        }
        gg()
        {
            console.log(this.recipient);
        }
        ScrollToBottom() {
            this.myScrollContainer.nativeElement.scrollTop += 10000;
        
         }
}