import {Component} from '@angular/core';
import {NavLoginComponent} from '../index';
import {Router,ActivatedRoute} from '@angular/router';
import { UserService } from '../../_services/index';
@Component({
    moduleId:module.id,
    selector:'navmenu',
    templateUrl:'nav.component.html',
    styleUrls:['nav.component.css']
})
export class NavComponent{
    str:string="";
    
    constructor(private route:Router,private activatedRoute:ActivatedRoute)
    {
        
    }
    search()
    {
        this.route.navigate(['/searchPerson',this.str]);
    }
    
}