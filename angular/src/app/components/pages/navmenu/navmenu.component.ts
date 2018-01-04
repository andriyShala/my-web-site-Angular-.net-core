import {Component} from '@angular/core';
import {Router} from '@angular/router';
@Component({
    moduleId:module.id,
    selector:'navigationmenu',
    templateUrl:'navmenu.component.html',
    styleUrls:['navmenu.component.css']
})
export class NavMenuComponent{
    constructor(private router:Router)
    {

    }
    butLoginClick(event)
    {
        this.router.navigate(['/register']);
    }
    
}