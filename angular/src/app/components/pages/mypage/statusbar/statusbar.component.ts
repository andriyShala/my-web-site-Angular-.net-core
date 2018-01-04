
import {Component,Input} from '@angular/core';
import {Router} from '@angular/router';
import './statusbar.component.less';
@Component({
    moduleId:module.id,
    selector:'mypage-statusbar',
    templateUrl:'statusbar.component.html'
    
})
export class StatusBarComponent{
    @Input() firstname: string = "Default Default";
    @Input() lastname: string = "Default Default";

}
