import {Component,OnInit,Input,Output,EventEmitter } from '@angular/core';
import {DomSanitizer,SafeHtml} from '@angular/platform-browser';

import './chat.component.less';
import { setTimeout } from 'timers';
@Component({
    moduleId:module.id,
    selector:'chatperson',
    templateUrl:'chat.component.html'
})
export class ChatPersonComponent implements OnInit{
      @Input() chatid:string="0";
    @Input() firstName: string = "FNamedsd";
     @Input() personimage: string = "https://cdn.pixabay.com/photo/2014/01/30/10/50/animal-254848_960_720.jpg";
    @Input() lastName: string = "LName";
    @Input() state: string = "state";
    @Input() lastTime: string = "lastTime";
    @Input() lastMessage: string = "lastMessage";
    @Output() myclick:EventEmitter<any>=new EventEmitter();
    public persimg:SafeHtml;
    public imgObj: Object = {
        src: 'path/to/image.jpg'
      };
    MyClickEvent(){
        this.myclick.emit(this.chatid);
    }
    constructor(private sanitizer: DomSanitizer)
    {

    }
    ngOnInit()
    {
        
    }
    
}